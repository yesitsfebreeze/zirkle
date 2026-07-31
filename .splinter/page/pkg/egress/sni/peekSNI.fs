// §head page/pkg/egress/sni.go:47-75 peekSNI
// §sig func peekSNI(conn net.Conn) (peeked []byte, sni string, err error)
	// TLS record header: 1 byte content_type, 2 bytes version, 2 bytes length.
	hdr := make([]byte, 5)
	if _, err := io.ReadFull(conn, hdr); err != nil {
		return hdr, "", err
	}

	// content_type 0x16 = Handshake.  Anything else is not TLS.
	if hdr[0] != 0x16 {
		return hdr, "", ErrNotTLS
	}

	recordLen := int(binary.BigEndian.Uint16(hdr[3:5]))
	if recordLen == 0 || recordLen > sniPeekLimit {
		return hdr, "", errors.New("egress: TLS record length out of range")
	}

	body := make([]byte, recordLen)
	if _, err := io.ReadFull(conn, body); err != nil {
		return append(hdr, body...), "", err
	}

	peeked = append(hdr, body...)
	sni, perr := parseClientHelloSNI(body)
	if perr != nil {
		return peeked, "", perr
	}
	return peeked, sni, nil
// §foot page/pkg/egress/sni.go peekSNI