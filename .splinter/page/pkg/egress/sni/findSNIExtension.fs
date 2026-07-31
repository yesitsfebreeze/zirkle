// §head page/pkg/egress/sni.go:142-155 findSNIExtension
// §sig func findSNIExtension(exts []byte) (string, error)
	for len(exts) >= 4 {
		extType := binary.BigEndian.Uint16(exts[0:2])
		extDataLen := int(binary.BigEndian.Uint16(exts[2:4]))
		if 4+extDataLen > len(exts) {
			return "", errors.New("egress: extension data overflow")
		}
		if extType == 0x0000 {
			return parseSNIExtension(exts[4 : 4+extDataLen])
		}
		exts = exts[4+extDataLen:]
	}
	return "", ErrNoSNI
// §foot page/pkg/egress/sni.go findSNIExtension