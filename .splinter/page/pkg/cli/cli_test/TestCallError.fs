// §head page/pkg/cli/cli_test.go:250-288 TestCallError
// §sig func TestCallError(t *testing.T)
	dir := t.TempDir()
	socketPath := filepath.Join(dir, "error.sock")

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

		resp := jsonResponse{ID: req.ID, Error: "something went wrong"}
		data, _ := json.Marshal(resp)
		fmt.Fprintf(conn, "%s\n", data)
	}()

	c := New(socketPath)
	if err := c.Dial(); err != nil {
		t.Fatal(err)
	}
	defer c.Close()

	_, err = c.Call("fail", nil)
	if err == nil || err.Error() != "something went wrong" {
		t.Fatalf("expected 'something went wrong', got: %v", err)
	}
	<-serverDone
// §foot page/pkg/cli/cli_test.go TestCallError