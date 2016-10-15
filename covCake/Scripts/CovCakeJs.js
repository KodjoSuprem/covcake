function LimitFieldLength(fld, len) {

    if (fld.value.length > len) { fld.value = fld.value.substr(0, len); }
}