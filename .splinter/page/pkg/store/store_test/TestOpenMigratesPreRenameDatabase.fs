// §head page/pkg/store/store_test.go:227-305 TestOpenMigratesPreRenameDatabase
// §sig func TestOpenMigratesPreRenameDatabase(t *testing.T)
	path := filepath.Join(t.TempDir(), "legacy.db")

	old, err := sql.Open("sqlite", path)
	if err != nil {
		t.Fatal(err)
	}
	_, err = old.Exec(`
CREATE TABLE oorb (
    id         TEXT PRIMARY KEY,
    prompt     TEXT NOT NULL,
    mode       TEXT NOT NULL,
    state      TEXT NOT NULL DEFAULT 'created',
    recap      TEXT NOT NULL DEFAULT '',
    created_at INTEGER NOT NULL,
    updated_at INTEGER NOT NULL
);
CREATE TABLE checkpoint (
    oorb_id    TEXT NOT NULL,
    turn       INTEGER NOT NULL,
    state      BLOB NOT NULL,
    created_at INTEGER NOT NULL,
    PRIMARY KEY (oorb_id, turn)
);
CREATE TABLE fault (
    id         INTEGER PRIMARY KEY AUTOINCREMENT,
    orb_id     TEXT NOT NULL DEFAULT '',
    kind       TEXT NOT NULL,
    site       TEXT NOT NULL,
    msg        TEXT NOT NULL,
    stack      TEXT NOT NULL DEFAULT '',
    created_at INTEGER NOT NULL
);
PRAGMA user_version = 3;`)
	if err != nil {
		t.Fatal(err)
	}
	if _, err := old.Exec(
		"INSERT INTO oorb (id, prompt, mode, state, recap, created_at, updated_at) VALUES ('legacy','p','smart','done','r',1,1)"); err != nil {
		t.Fatal(err)
	}
	if _, err := old.Exec(
		"INSERT INTO checkpoint (oorb_id, turn, state, created_at) VALUES ('legacy', 1, X'6162', 1)"); err != nil {
		t.Fatal(err)
	}
	if _, err := old.Exec(
		"INSERT INTO fault (orb_id, kind, site, msg, stack, created_at) VALUES ('legacy','error','site','msg','',1)"); err != nil {
		t.Fatal(err)
	}
	old.Close()

	s, err := Open(path)
	if err != nil {
		t.Fatalf("Open on a pre-rename database: %v", err)
	}
	defer s.Close()

	p, err := s.Load("legacy")
	if err != nil {
		t.Fatalf("Load after rename migration: %v", err)
	}
	if p.Prompt != "p" {
		t.Fatalf("row lost in rename migration: %+v", p)
	}
	state, err := s.LoadCheckpoint("legacy", 1)
	if err != nil {
		t.Fatalf("LoadCheckpoint after rename migration: %v", err)
	}
	if string(state) != "ab" {
		t.Fatalf("checkpoint state = %q, want \"ab\"", state)
	}
	faults, err := s.Faults(10)
	if err != nil {
		t.Fatalf("Faults after rename migration: %v", err)
	}
	if len(faults) != 1 || faults[0].PodID != "legacy" {
		t.Fatalf("faults = %+v, want one row keyed legacy", faults)
	}
// §foot page/pkg/store/store_test.go TestOpenMigratesPreRenameDatabase