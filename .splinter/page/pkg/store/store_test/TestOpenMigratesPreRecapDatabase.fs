// §head page/pkg/store/store_test.go:171-222 TestOpenMigratesPreRecapDatabase
// §sig func TestOpenMigratesPreRecapDatabase(t *testing.T)
	path := filepath.Join(t.TempDir(), "old.db")

	old, err := sql.Open("sqlite", path)
	if err != nil {
		t.Fatal(err)
	}
	_, err = old.Exec(`
CREATE TABLE pod (
    id         TEXT PRIMARY KEY,
    prompt     TEXT NOT NULL,
    mode       TEXT NOT NULL,
    state      TEXT NOT NULL DEFAULT 'created',
    created_at INTEGER NOT NULL,
    updated_at INTEGER NOT NULL
);
PRAGMA user_version = 2;`)
	if err != nil {
		t.Fatal(err)
	}
	if _, err := old.Exec(
		"INSERT INTO pod (id, prompt, mode, state, created_at, updated_at) VALUES ('a1','p','smart','done',1,1)"); err != nil {
		t.Fatal(err)
	}
	old.Close()

	s, err := Open(path)
	if err != nil {
		t.Fatalf("Open on a pre-recap database: %v", err)
	}
	defer s.Close()

	// The pre-existing row must survive and Save must work.
	o, err := s.Load("a1")
	if err != nil {
		t.Fatalf("Load after migration: %v", err)
	}
	if o.Prompt != "p" {
		t.Fatalf("row lost in migration: %+v", o)
	}
	o.Recap = "migrated fine"
	if err := s.Save(o); err != nil {
		t.Fatalf("Save after migration: %v", err)
	}
	again, err := s.Load("a1")
	if err != nil {
		t.Fatal(err)
	}
	if again.Recap != "migrated fine" {
		t.Fatalf("recap = %q, want it persisted", again.Recap)
	}
// §foot page/pkg/store/store_test.go TestOpenMigratesPreRecapDatabase