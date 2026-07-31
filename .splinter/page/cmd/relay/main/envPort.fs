// §head page/cmd/relay/main.go:253-260 envPort
// §sig func envPort(key string, def int) int
	if v := os.Getenv(key); v != "" {
		if n, err := strconv.Atoi(v); err == nil {
			return n
		}
	}
	return def
// §foot page/cmd/relay/main.go envPort