using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using covCake.DataAccess;
using covCake.Models;

namespace covCake.Controllers
{
    [RedirectAuthorize]
    public class MessagesController : BaseController
    {

        [GetModelError]
        [PageTitle("Mes Messages")]
        public ActionResult ShowMessage(int msgId, int? page)
        {
            IMessagePrive msg;
            IQueryable<IMessagePrive> msgConnexes; //Derniers messages reçus par cette personne
            IQueryable<IMessagePrive> msgThread; 
            try
            {
                 msg = Data.MessageDataAccess.GetMessage(msgId);
                 msgThread = Data.MessageDataAccess.GetAllMsgThread(msgId) ;
             
                 //Seul les messages qui concernent CurrentUser sont mit à LU 
                 foreach (IMessagePrive mt in msgThread)
                 {
                     if (msg.ToUserId == CurrentUser.UserId)
                         mt.IsToNewMessage = false;
                     else if (msg.FromUserId == CurrentUser.UserId)
                         mt.IsFromNewMessage = false;
                 }

                 Data.SubmitChanges();
                 //on récupere le thread des messages reçus
                 DateTime lastYear = new DateTime(msg.DateMessage.Year - 1, msg.DateMessage.Month, msg.DateMessage.Day);
           
                msgConnexes = Data.MessageDataAccess.GetAllThreadHeads()
                    .GetAllMessagesFrom(msg.FromUserId)
                    .GetAllMessagesBetween(lastYear, msg.DateMessage)
                    .OrderByDescending(m => m.DateMessage);
                /*
                 msgConnexes = Data.MessageDataAccess
                     .GetAllMessagesFrom(msg.FromUserId)
                     .Where(m => m.MsgId != msg.MsgId) 
                     .GetAllMessagesBetween(lastYear,msg.DateMessage)
                     .OrderByDescending(m => m.DateMessage);
                 * */
            }
            catch(Exception ex)
            {
                var errmsg =  "Un problème est survenu lors de la récupération du message.";
                return Error(errmsg);
                /*
                ErrorRedirectViewData error = new ErrorRedirectViewData(){
                    DelaySeconds    = 5,
                    ErrorMsg        = "Le message n'a pas été trouvé",
                    RedirectUrl     = Url.Action("Index"),
                    Title           = "Message"
                };
                */
                // return View("ErrorRedirect", error);
               
            }

            ShowMessageViewData msgViewData = new ShowMessageViewData()
            {
                Msg = msg,
                MsgThread = msgThread,
                MsgConnexes = msgConnexes.ToPagedList(page ?? 0)
            };
            return View(msgViewData);
        }
         

        [PageTitle("Mes Messages")]
        [GetModelError]
        public ActionResult Index(int? folder, int? page)
        {
            int pageIdx = page ?? 0;
            folder = folder ?? TypeBoiteMessage.Inbox.GetValue().ToInt();
            //if(pageIdx>0) pageIdx--; 
            
            TypeBoiteMessage typeBoite = folder.EnumParse<TypeBoiteMessage>();
            InboxViewData inboxdata = new InboxViewData();
            inboxdata.TypeBoite = typeBoite; 
            switch(inboxdata.TypeBoite)
            {
                case TypeBoiteMessage.Inbox:
                    IQueryable<IMessagePrive> msgs = this.CurrentUser.MessagesRecus.GetAllThreadsNotDeleted(inboxdata.TypeBoite).OrderByDescending(m => m.DateMessage);
                    inboxdata.Messages = new PagedList<IMessagePrive>(msgs, pageIdx, 10);
                    break;
                case TypeBoiteMessage.Outbox:
                    IQueryable<IMessagePrive> msgsSent = this.CurrentUser.MessagesEnvoyes.GetAllThreadsNotDeleted(inboxdata.TypeBoite).OrderByDescending(m => m.DateMessage);
                    inboxdata.Messages = new PagedList<IMessagePrive>(msgsSent, pageIdx, 10);
                    break;
                case TypeBoiteMessage.Trash:
                    //TODO: ajouter dans le modele une corbeille à message
                    break;
                case TypeBoiteMessage.Saved:
                    //TODO: TypeBoiteMessage dans le model un champ message sauvegardé
                    //break;
                default:
                    inboxdata.TypeBoite = TypeBoiteMessage.Inbox;
                    IQueryable<IMessagePrive> msgsDefault = this.CurrentUser.MessagesRecus.GetAllThreadsNotDeleted(inboxdata.TypeBoite).OrderByDescending(m => m.DateMessage);
                    inboxdata.Messages = new PagedList<IMessagePrive>(msgsDefault, pageIdx, 10);
                    break;
            }

            //TODO: Bien vérifier que le transfert de model error marche

          
            return View(inboxdata);
        }

       
        /// <summary>
        /// Prépare la vue d'envoi de mesage relatif au projet
        /// AjaxOnly
        /// </summary>
        /// <param name="projetId"></param>
        /// <returns>Partial("SendMessageModal")</returns>
        [PageTitle("Message")]
        public ActionResult SendMessage(int? pId, string toUid )
        {
           
            //ViewData["title"] = "Message";
            Guid ToUserId = toUid.ToGuid();
            if (pId == null && toUid == null)
            {
                ViewData["title"] = "Messagerie";
                ViewData["message"] = "Impossible d'envoyer le message :(";
                return PartialView("BlankModal");
    
            }
            IUserProfile toUser = null;
            IProjet relatedProjet = null;
            if (ToUserId.IsEmpty() == false)
            {
                toUser = Data.UserDataAccess.GetUser(ToUserId);
                if (toUser == null)
                    ViewData["message"] = "Ce message ne peut être envoyé au destinataire indiqué :( ";

            }
            else if(pId.HasValue)
            {
                relatedProjet = Data.ProjetDataAccess.GetProjet(pId.Value);
                if(relatedProjet == null)
                    ViewData["message"] = "Le projet concernant ce message est introuvable :( ";
            }
            if(ViewData.ContainsKey("message"))
                return PartialView("BlankModal");
            SendMessageViewData sendMessageData = new SendMessageViewData();

            if(relatedProjet != null)
            {
                sendMessageData.RelatedProjet = relatedProjet;
                sendMessageData.ToUserProfile = relatedProjet.OwnerUserProfile;

                sendMessageData.relatedProjetId = relatedProjet.IdProjet.ToString();
                sendMessageData.toUserId = relatedProjet.OwnerUserProfile.UserId.ToString();

                sendMessageData.sujet = "A propos du voyage n°" + sendMessageData.RelatedProjet.IdProjet + ": " + sendMessageData.RelatedProjet.GetShortDisplayName();
                
            }

            if (toUser != null)
            {
                sendMessageData.ToUserProfile = toUser;

                sendMessageData.toUserId = toUser.UserId.ToString();

                sendMessageData.sujet = "";
       
            }
                return PartialView("SendMessageModal", sendMessageData); 
         
        }
        

        [AcceptVerbs(HttpVerbs.Post)]
        [Obsolete("Pas utiliser")]
        private ActionResult DeleteThread(int? firstMsgId, string folder)
        {
            throw new NotImplementedException("pas de scénario 18/08/2009");

            if (firstMsgId != null)
            {
                try
                {
                    TypeBoiteMessage boite = folder.EnumParse<TypeBoiteMessage>();

                    Data.MessageDataAccess.SetAllMsgThreadDeleted(firstMsgId.Value, CurrentUser.UserId, boite);
                }
                catch (Exception ex)
                {
                 
                    TempData["ModelError"] = this.ModelState;
                    return RedirectToAction("Index");//Json(false);
                }
            }
            return RedirectToAction("Index");
          
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteAll(string[] msgIds, string folder)
        {

            if (msgIds != null)
            {
                try
                {
                    msgIds = msgIds.Where(s => s != "false").ToArray();//

                    List<IMessagePrive> msgToDelete = new List<IMessagePrive>();

                    foreach (string msgId in msgIds)
                    {
                        msgToDelete.Add(Data.MessageDataAccess.GetMessage(int.Parse(msgId)));
                    }

                    //on s'assure qu'on supprime que les messages qui  appartiennent à l'util
                    IEnumerable<IMessagePrive> msgsFiltered = msgToDelete.Where(m => m.ToUserId == CurrentUser.UserId || m.FromUserId == CurrentUser.UserId);
                    
                    //Data.MessageDataAccess.DeleteMessages(msgsFiltered);

                    TypeBoiteMessage boite = folder.EnumParse<TypeBoiteMessage>();

                    Data.MessageDataAccess.SetDeleted(msgsFiltered, CurrentUser.UserId, boite);
                    msgToDelete = null;
                    msgsFiltered = null;

                }
                catch (Exception ex)
                {
                    Dictionary<string, string> modelError = new Dictionary<string, string>();
                    modelError.Add("_FORM", "Certains messages n'ont pas pu être supprimé");
                    TempData["ModelError"] = modelError;
                    //return RedirectToAction("Index");//Json(false);
                }
            }
            return RedirectToAction("Index", new { folder = folder });
        }

        [Obsolete()]
        public ActionResult DeleteAlll(string jsonMsgIds)
        {
            try
            {
                // Get a deserializer
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                // You could deserialize to a name-value pair, and access it as a weakly-typed object
               // IDictionary<string, object> msgIds = serializer.Deserialize<IDictionary<string, object>>(jsonMsgIds);
                int[] msgIds = serializer.Deserialize<int[]>(jsonMsgIds);

                //on s'assure qu'on supprime que les messages qui  appartiennent à l'util
                List<IMessagePrive> msgToDelete = new List<IMessagePrive>();

                foreach(var msgId in msgIds)
                {
                   msgToDelete.Add(Data.MessageDataAccess.GetMessage(msgId));

                }
                IEnumerable<IMessagePrive> msgsFiltered = msgToDelete.Where(m => m.ToUserId == CurrentUser.UserId || m.FromUserId == CurrentUser.UserId);
                Data.MessageDataAccess.DeleteMessages(msgsFiltered);
                msgToDelete = null;
                msgsFiltered = null;
                //Data.MessageDataAccess.DeleteMessage(msgId);
            }
            catch(Exception ex)
            {
                return Json(false);
            }
            return Json(true);
        }

       

        /// <summary>
        /// JAJAX Handle ajax only
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
//        [TrimActionParams]
        public ActionResult SendSingleMessage(SendMessageViewData sentMsgData )
        {
            //Si le message répond à un autre on met la tête de thread a non lu.
            throw new Exception("Exception non catchée");

            const int MAXMESSAGELENGHT = 4000;
            int newMsgId = -1;
            try
            {
                /*
                    System.Diagnostics.Debug.WriteLine("homosexuel : " + sentMsgData.sujet);
                    System.Diagnostics.Trace.WriteLine("homosexuel : " + sentMsgData.sujet);
                */
                //bool ajax = Request.IsAjaxRequest();
                //C'est moche mais sa saoul //21/07/09 20:12
                var sujet = sentMsgData.sujet;
                var message = sentMsgData.message;
                var toUserId = sentMsgData.toUserId;
                var fromUserId = sentMsgData.fromUserId;
                var relatedProjetId = sentMsgData.relatedProjetId;//.Trim();
                var responseMsgId = sentMsgData.responseMsgId;
                int? relProjId = (!relatedProjetId.IsNullOrEmpty()) ? (int?)int.Parse(relatedProjetId) : null;
                int? respIdMsg = (!responseMsgId.IsNullOrEmpty()) ? (int?)int.Parse(responseMsgId) : null;
                
                newMsgId = respIdMsg ?? newMsgId;

                if (message.Length > MAXMESSAGELENGHT)
                    this.ModelState.AddModelError("message", "Votre message est trop long. (plus de "+MAXMESSAGELENGHT+" caractères)");
                if (!this.ModelState.IsValid)
                    throw new Exception("Message trop long");


                if (sujet.IsNullOrEmpty() && respIdMsg.HasValue)
                {
                    IMessagePrive originalMsg = Data.MessageDataAccess.GetMessage(respIdMsg.Value);
                    originalMsg.IsFromNewMessage = true; // le thread est considéré comme nouveau
                    originalMsg.IsToNewMessage = true;
                    sujet = "RE : "+originalMsg.SujetMessage;
                }

             
                //sujet généré automatiqment si nul
                if (sujet.IsNullOrEmpty() && relProjId != null)
                    sujet = GetDefaultMessageSubject(relProjId.Value);
                else if ((sujet).IsNullOrEmpty())
                    sujet = "(sans sujet)";
      
                IMessagePrive newMsg = InsertNewMessage(new Guid(fromUserId), new Guid(toUserId), sujet, message, respIdMsg, relProjId);
                newMsgId =  newMsg.MsgId;

              
                //Envoi de l'email de notification
                this.CovCakeMailer.SendNewMessageNotify(newMsg);

            }
            catch(Exception e)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(false);
                }
                else
                {
                    this.ModelState.AddModelError("_FORMMSG", "Le message n'a pas pu être envoyé");
                    TempData["ModelError"] = this.ModelState;
                    return RedirectToAction("ShowMessage", new { msgId = newMsgId });
                }

              //  return PartialView("sendMessageError");
            }
             
              if(Request.IsAjaxRequest())
                    return Json(true);// new TextViewResult("Le messsage a bien été envoyé"); // PartialView("SendMessageModal", sendMessageData);
              else
                    return RedirectToAction("ShowMessage", new { msgId = newMsgId });


         }

        private IMessagePrive InsertNewMessage(Guid fromUserId, Guid toUserId, string sujet,string message , int? responseMsgId, int? projetRelatedId)
        {
           
            IMessagePrive newMsg = Data.MessageDataAccess.CreateMessage();
            newMsg.DateMessage = DateTime.Now;
            newMsg.FromUserId = fromUserId;
            newMsg.ToUserId = toUserId;
            newMsg.ProjetRelatedId = projetRelatedId;
           
            newMsg.IsFromNewMessage = false;
            newMsg.IsToNewMessage = true;
            newMsg.SujetMessage = sujet;
            newMsg.Ip = Request.UserHostAddress;
            newMsg.TextMessage = message;
            newMsg.MsgResponseId = responseMsgId;
            //Insert in DB
            Data.MessageDataAccess.InsertMessage(newMsg);
            return newMsg;
        }

        private string GetDefaultSubject()
        {
            return "Sans Sujet";
        }

        private string GetDefaultMessageSubject(int projId)
        {
            IProjet proj = Data.ProjetDataAccess.GetProjet(projId);
            if(proj != null)
                return "Message à propos du voyage n°" + projId +": " + proj.GetShortDisplayName();
            return "(Sans sujet)"; 

        }
    }
}
