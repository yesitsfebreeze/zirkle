// §head page/pkg/subagent/executor_test.go:134-147 TestPodRunIgnoresBannerNoise
// §sig func TestPodRunIgnoresBannerNoise(t *testing.T)
	o := Pod{
		Host:    "pod-1",
		Command: shim(t, `echo "Welcome to Ubuntu"; echo '{"success":true,"summary":"ok","output":"o","tokens":7}'`),
	}

	res, err := o.Run(context.Background(), Config{Prompt: "x", Timeout: 5 * time.Second})
	if err != nil {
		t.Fatalf("Pod.Run: %v", err)
	}
	if res.Summary != "ok" || res.Tokens != 7 {
		t.Fatalf("got %+v", res)
	}
// §foot page/pkg/subagent/executor_test.go TestPodRunIgnoresBannerNoise