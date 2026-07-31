// §source page/pkg/config/config.go
// Package config layers configuration: built-in defaults, config file
// (~/.relay/config.toml), environment variables (RELAY_*), and CLI flags, in
// that order — later wins.  The package owns the structs and the Load
// pipeline; cmd/relay defines the flags and applies their values last.
package config

import (
	"embed"
	"fmt"
	"os"
	"path/filepath"
	"strconv"

	"github.com/BurntSushi/toml"
	"github.com/feb/relay/pkg/egress"
)

//go:embed default.toml
var defaultConfigFS embed.FS

// Config is the full daemon configuration.  TOML tags match the config file
// section names in docs/specs/config.md.
type Config struct {
	Daemon   DaemonConfig   `toml:"daemon"`
	LLM      LLMConfig      `toml:"llm"`
	Store    StoreConfig    `toml:"store"`
	Sched    SchedConfig    `toml:"sched"`
	Webhook  WebhookConfig  `toml:"webhook"`
	Sandbox  SandboxConfig  `toml:"sandbox"`
	Log      LogConfig      `toml:"log"`
	Theme    ThemeConfig    `toml:"theme"`
	Timeline TimelineConfig `toml:"timeline"`
}

type DaemonConfig struct {
	Port   int    `toml:"port"`
	Socket string `toml:"socket"`
}

type LLMConfig struct {
	Provider  string `toml:"provider"`
	APIKey    string `toml:"api_key"`
	Model     string `toml:"model"`
	MaxTokens int    `toml:"max_tokens"`
}

type StoreConfig struct {
	Dir string `toml:"dir"`
}

type SchedConfig struct {
	Interval int `toml:"interval"`
}

type WebhookConfig struct {
	Secret string `toml:"secret"`
}

type SandboxConfig struct {
	Mode           string   `toml:"mode"`
	SizeMB         int      `toml:"size_mb"`
	Ephemeral      bool     `toml:"ephemeral"`
	AllowedDomains []string `toml:"allowed_domains"`
	DeniedDomains  []string `toml:"denied_domains"`
	RW             []string `toml:"rw"`
}

type LogConfig struct {
	Level string `toml:"level"`
	JSON  bool   `toml:"json"`
}

// ThemeConfig controls whether relay overrides the terminal's ANSI palette
// with truecolour hexes. Custom=false (default) defers to the terminal:
// relay renders through ANSI 0-8 and inherits whatever the terminal's first
// row is. Custom=true applies Colors over the roles named there; any role
// not listed keeps its ANSI slot, so a custom theme can fix one colour or
// all of them.
//
// Recognised keys: foreground, primary, attention, muted, secondary,
// failure, surface, rule.
type ThemeConfig struct {
	Custom bool              `toml:"custom"`
	Colors map[string]string `toml:"colors"`
}

// TimelineConfig controls the non-selectable frame headers in the pod list.
// Frame buckets the list by day (default), week, month or hour; DayStart moves
// the day rollover off midnight ("04:00" keeps a long night in one frame).
// ShowCount, ShowStates and ShowSpan are the tick boxes in the TUI settings
// screen: each drops one part of the header's roll-up.
type TimelineConfig struct {
	Enabled    bool   `toml:"enabled"`
	Frame      string `toml:"frame"`
	DayStart   string `toml:"day_start"`
	ShowCount  bool   `toml:"show_count"`
	ShowStates bool   `toml:"show_states"`
	ShowSpan   bool   `toml:"show_span"`
}

// Default returns the built-in defaults: Ollama on localhost, RAM-backed
// sandbox, deny-by-default egress.  No file, no env, no flags.
func Default() Config {
// §.splinter/page/pkg/config/config/Default.fs
}

// DefaultPath returns ~/.relay/config.toml, the conventional config file
// location.  An error here means the home directory is unavailable.
func DefaultPath() (string, error) {
// §.splinter/page/pkg/config/config/DefaultPath.fs
}

// EnsureDefault writes the embedded default.toml to ~/.relay/config.toml when no
// file exists yet, so the user has a real file to edit from day one. A missing
// parent dir is created. An existing file is never overwritten.
func EnsureDefault() (string, error) {
// §.splinter/page/pkg/config/config/EnsureDefault.fs
}

// Load reads defaults → config file → environment, in that order.  A missing
// file is not an error; the caller applies CLI flags on top of the result.
func Load(path string) (Config, error) {
// §.splinter/page/pkg/config/config/Load.fs
}

// applyEnv overlays RELAY_* environment variables on the config.
func applyEnv(c *Config) {
// §.splinter/page/pkg/config/config/applyEnv.fs
}

// expandHome replaces a leading ~ with the user's home directory.  Runs
// after env layering so RELAY_STORE_DIR=~/.relay works too.
func expandHome(c *Config) {
// §.splinter/page/pkg/config/config/expandHome.fs
}

func expandTilde(p, home string) string {
// §.splinter/page/pkg/config/config/expandTilde.fs
}

func atoiOr(s string, def int) int {
// §.splinter/page/pkg/config/config/atoiOr.fs
}

// EgressPolicy builds the egress policy from sandbox config.  This is the
// path from config to the enforcement layer F13 builds: allowed_domains
// and denied_domains reach pkg/egress.Policy directly.
func (c *Config) EgressPolicy() *egress.Policy {
// §.splinter/page/pkg/config/config/Config.EgressPolicy.fs
}
