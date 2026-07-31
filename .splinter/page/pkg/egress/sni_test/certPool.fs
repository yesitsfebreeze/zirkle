// §head page/pkg/egress/sni_test.go:48-53 certPool
// §sig func certPool(cert tls.Certificate) *x509.CertPool
	pool := x509.NewCertPool()
	parsed, _ := x509.ParseCertificate(cert.Certificate[0])
	pool.AddCert(parsed)
	return pool
// §foot page/pkg/egress/sni_test.go certPool