// §head page/pkg/config/config_test.go:180-209 TestEgressPolicyFromConfig
// §sig func TestEgressPolicyFromConfig(t *testing.T)
	path := writeConfigFile(t, `
[sandbox]
allowed_domains = ["good.com", "*.safe.org"]
denied_domains = ["bad.com"]
`)
	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	pol := c.EgressPolicy()
	if pol == nil {
		t.Fatal("nil policy")
	}
	if !reflect.DeepEqual(pol.AllowedDomains, []string{"good.com", "*.safe.org"}) {
		t.Errorf("allowed_domains = %v", pol.AllowedDomains)
	}
	if !reflect.DeepEqual(pol.DeniedDomains, []string{"bad.com"}) {
		t.Errorf("denied_domains = %v", pol.DeniedDomains)
	}
	// Verify it's a real egress.Policy that actually works.
	allowed := pol.Allow("good.com")
	if !allowed {
		t.Error("Allow(good.com) = false, want true")
	}
	denied := pol.Allow("bad.com")
	if denied {
		t.Error("Allow(bad.com) = true, want false")
	}
// §foot page/pkg/config/config_test.go TestEgressPolicyFromConfig