// §head page/pkg/egress/sni.go:159-180 parseSNIExtension
// §sig func parseSNIExtension(data []byte) (string, error)
	if len(data) < 2 {
		return "", errors.New("egress: short SNI extension")
	}
	listLen := int(binary.BigEndian.Uint16(data[0:2]))
	if 2+listLen > len(data) {
		return "", errors.New("egress: SNI list overflow")
	}
	list := data[2 : 2+listLen]
	for len(list) >= 5 {
		nameType := list[0]
		nameLen := int(binary.BigEndian.Uint16(list[1:3]))
		if 3+nameLen > len(list) {
			return "", errors.New("egress: SNI name overflow")
		}
		if nameType == 0x00 { // host_name
			return string(list[3 : 3+nameLen]), nil
		}
		list = list[3+nameLen:]
	}
	return "", ErrNoSNI
// §foot page/pkg/egress/sni.go parseSNIExtension