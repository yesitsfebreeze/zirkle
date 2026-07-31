// §head page/pkg/egress/sni.go:79-138 parseClientHelloSNI
// §sig func parseClientHelloSNI(body []byte) (string, error)
	// Handshake header: type (1 byte) + length (3 bytes).
	if len(body) < 4 {
		return "", errors.New("egress: short handshake header")
	}
	if body[0] != 0x01 { // ClientHello
		return "", errors.New("egress: not a ClientHello")
	}

	// ClientHello body starts after the 4-byte handshake header.
	ch := body[4:]

	// version (2) + random (32) = 34 bytes minimum before session_id.
	if len(ch) < 35 {
		return "", errors.New("egress: short ClientHello before session ID")
	}
	pos := 34 // skip version + random

	// Session ID: 1-byte length + data.
	sidLen := int(ch[pos])
	pos++
	if pos+sidLen > len(ch) {
		return "", errors.New("egress: session ID overflow")
	}
	pos += sidLen

	// Cipher suites: 2-byte length + data.
	if pos+2 > len(ch) {
		return "", errors.New("egress: short ClientHello at cipher suites")
	}
	csLen := int(binary.BigEndian.Uint16(ch[pos:]))
	pos += 2
	if pos+csLen > len(ch) {
		return "", errors.New("egress: cipher suites overflow")
	}
	pos += csLen

	// Compression methods: 1-byte length + data.
	if pos+1 > len(ch) {
		return "", errors.New("egress: short ClientHello at compression")
	}
	cmLen := int(ch[pos])
	pos++
	if pos+cmLen > len(ch) {
		return "", errors.New("egress: compression methods overflow")
	}
	pos += cmLen

	// Extensions: 2-byte total length + data.  Absent = no extensions.
	if pos+2 > len(ch) {
		return "", ErrNoSNI
	}
	extLen := int(binary.BigEndian.Uint16(ch[pos:]))
	pos += 2
	if pos+extLen > len(ch) {
		return "", errors.New("egress: extensions overflow")
	}

	return findSNIExtension(ch[pos : pos+extLen])
// §foot page/pkg/egress/sni.go parseClientHelloSNI