// §head page/pkg/config/config_test.go:261-272 TestThemeDefaultIsDesignedPalette
// §sig func TestThemeDefaultIsDesignedPalette(t *testing.T)
	c := Default()
	if !c.Theme.Custom {
		t.Error("default theme.custom = false, want true (designed palette)")
	}
	if c.Theme.Colors["primary"] != "#BA8CFF" {
		t.Errorf("default primary = %q, want #BA8CFF", c.Theme.Colors["primary"])
	}
	if c.Theme.Colors["foreground"] != "#FFFFFF" {
		t.Errorf("default foreground = %q, want #FFFFFF", c.Theme.Colors["foreground"])
	}
// §foot page/pkg/config/config_test.go TestThemeDefaultIsDesignedPalette