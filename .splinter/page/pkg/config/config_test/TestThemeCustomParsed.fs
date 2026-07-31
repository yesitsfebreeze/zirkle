// §head page/pkg/config/config_test.go:235-256 TestThemeCustomParsed
// §sig func TestThemeCustomParsed(t *testing.T)
	path := writeConfigFile(t, `
[theme]
custom = true
[theme.colors]
foreground = "#FFFFFF"
primary = "#BA8CFF"
`)
	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	if !c.Theme.Custom {
		t.Error("theme.custom = false, want true")
	}
	if c.Theme.Colors["foreground"] != "#FFFFFF" {
		t.Errorf("foreground = %q, want #FFFFFF", c.Theme.Colors["foreground"])
	}
	if c.Theme.Colors["primary"] != "#BA8CFF" {
		t.Errorf("primary = %q, want #BA8CFF", c.Theme.Colors["primary"])
	}
// §foot page/pkg/config/config_test.go TestThemeCustomParsed