// §head page/pkg/cli/cli_test.go:159-196 TestKill
// §sig func TestKill(t *testing.T)
	dir := t.TempDir()
	socketPath := filepath.Join(dir, "kill.sock")

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
			return
		}
		defer conn.Close()

		var req jsonRequest
		json.NewDecoder(conn).Decode(&req)

		resp := jsonResponse{ID: req.ID, Result: "killed"}
		data, _ := json.Marshal(resp)
		fmt.Fprintf(conn, "%s\n", data)
	}()

	c := New(socketPath)
	if err := c.Dial(); err != nil {
		t.Fatal(err)
	}
	defer c.Close()

	if err := c.Kill("test-pod"); err != nil {
		t.Fatal(err)
	}
	<-serverDone
// §foot page/pkg/cli/cli_test.go TestKill