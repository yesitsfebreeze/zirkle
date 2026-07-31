// §head page/cmd/relay/main.go:472-484 serveDaemon
// §sig func serveDaemon(lis net.Listener, s store.Store, l llm.LLM)
	for {
		conn, err := lis.Accept()
		if err != nil {
			log.Printf("socket accept: %v", err)
			return
		}
		go func() {
			defer fault.Guard(s, "", "daemon.conn")
			handleConn(conn, s, l)
		}()
	}
// §foot page/cmd/relay/main.go serveDaemon