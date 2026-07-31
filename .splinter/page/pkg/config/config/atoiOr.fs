// §head page/pkg/config/config.go:245-251 atoiOr
// §sig func atoiOr(s string, def int) int
	n, err := strconv.Atoi(s)
	if err != nil {
		return def
	}
	return n
// §foot page/pkg/config/config.go atoiOr