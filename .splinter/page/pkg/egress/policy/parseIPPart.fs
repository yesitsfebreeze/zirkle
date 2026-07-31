// §head page/pkg/egress/policy.go:152-166 parseIPPart
// §sig func parseIPPart(part string) (uint64, bool)
	switch {
	case part == "":
		return 0, false
	case strings.HasPrefix(part, "0x"), strings.HasPrefix(part, "0X"):
		v, err := strconv.ParseUint(part[2:], 16, 64)
		return v, err == nil
	case len(part) > 1 && part[0] == '0':
		v, err := strconv.ParseUint(part[1:], 8, 64)
		return v, err == nil
	default:
		v, err := strconv.ParseUint(part, 10, 64)
		return v, err == nil
	}
// §foot page/pkg/egress/policy.go parseIPPart