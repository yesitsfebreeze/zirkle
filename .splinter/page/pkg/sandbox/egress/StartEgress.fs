// §head page/pkg/sandbox/egress.go:17-63 StartEgress
// §sig func StartEgress(spec *Spec, policy *egress.Policy) (func(), error)
	dir, err := os.MkdirTemp("", "relay-egress-")
	if err != nil {
		return nil, err
	}

	httpSock := filepath.Join(dir, "http.sock")
	socksSock := filepath.Join(dir, "socks5.sock")

	httpL, err := egress.Listen(httpSock)
	if err != nil {
		os.RemoveAll(dir)
		return nil, err
	}
	socksL, err := egress.Listen(socksSock)
	if err != nil {
		httpL.Close()
		os.RemoveAll(dir)
		return nil, err
	}

	httpP := egress.NewHTTPProxy(policy)
	socksP := egress.NewSOCKS5Proxy(policy)

	// Proxies stop when the listener is closed (cleanup).
	go httpP.Serve(httpL)
	go socksP.Serve(socksL)

	// Inject proxy env vars — tools inside the sandbox that understand unix
	// socket proxies can use these; the rest fail harmlessly in the empty
	// netns.
	spec.Env = append(spec.Env,
		"HTTP_PROXY=http://"+httpSock,
		"HTTPS_PROXY=http://"+httpSock,
		"ALL_PROXY=socks5://"+socksSock,
	)

	// Bind-mount the socket dir into the sandbox via the tools mechanism.
	spec.Tools = append(spec.Tools, dir)

	cleanup := func() {
		httpL.Close()
		socksL.Close()
		os.RemoveAll(dir)
	}
	return cleanup, nil
// §foot page/pkg/sandbox/egress.go StartEgress