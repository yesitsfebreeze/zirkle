// §head page/pkg/cli/cli_test.go:11-65 TestDialRoundTrip
// §sig func TestDialRoundTrip(t *testing.T)
	dir := t.TempDir()
	socketPath := filepath.Join(dir, "test.sock")

	// Start a mock server
	lis, err := net.Listen("unix", socketPath)
	if err != nil {
		t.Fatal(err)
	}
	defer lis.Close()

	serverDone := make(chan struct{})
	go func() {
		defer close(serverDone)
		conn, err := lis.Accept()
		if err != nil {
			t.Logf("server accept: %v", err)
			return
		}
		defer conn.Close()

		// Read request
		var req jsonRequest
		if err := json.NewDecoder(conn).Decode(&req); err != nil {
			t.Logf("server decode: %v", err)
			return
		}
		if req.Method != "ping" {
			t.Logf("unexpected method: %s", req.Method)
			return
		}

		// Send response
		resp := jsonResponse{ID: req.ID, Result: "pong"}
		data, _ := json.Marshal(resp)
		fmt.Fprintf(conn, "%s\n", data)
	}()

	// Client connects
	c := New(socketPath)
	if err := c.Dial(); err != nil {
		t.Fatal(err)
	}
	defer c.Close()

	result, err := c.Call("ping", nil)
	if err != nil {
		t.Fatal(err)
	}
	if result != "pong" {
		t.Fatalf("got %v, want pong", result)
	}

	<-serverDone
// §foot page/pkg/cli/cli_test.go TestDialRoundTrip