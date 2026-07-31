// §head page/pkg/tui/tui.go:1238-1252 parseHex
// §sig func parseHex(hex string) (r, g, b uint8, ok bool)
	hex = strings.TrimPrefix(hex, "#")
	if len(hex) != 6 {
		return 0, 0, 0, false
	}
	var v [3]uint8
	for i := 0; i < 3; i++ {
		n, err := strconv.ParseUint(hex[2*i:2*i+2], 16, 8)
		if err != nil {
			return 0, 0, 0, false
		}
		v[i] = uint8(n)
	}
	return v[0], v[1], v[2], true
// §foot page/pkg/tui/tui.go parseHex