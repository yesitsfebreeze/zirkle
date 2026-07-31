// §source page/pkg/llm/provider.go
package llm

import (
	"fmt"
	"os"
	"sort"
	"sync"
)

// DefaultProvider is Ollama: a local daemon needs no key and no spend, so the
// default path proves the system end to end before any cloud account exists.
const DefaultProvider = "ollama"

// providerFactory builds an LLM for one provider, given a model that may be
// empty to take the provider's own default. Each provider registers its
// factory once; New never grows another case.
type providerFactory func(model string) LLM

var (
	providersMu sync.RWMutex
	providers   = map[string]providerFactory{}
)

// Register adds a provider under id. A new provider is one file with one
// Register call — the switch is gone.
func Register(id string, f providerFactory) {
// §.splinter/page/pkg/llm/provider/Register.fs
}

func lookup(id string) (providerFactory, bool) {
// §.splinter/page/pkg/llm/provider/lookup.fs
}

func providerNames() []string {
// §.splinter/page/pkg/llm/provider/providerNames.fs
}

// New builds the LLM for a provider name. An empty provider falls back to
// RELAY_LLM_PROVIDER then DefaultProvider; an empty model to the provider's own
// default. Model is also overridable via RELAY_MODEL.
func New(provider, model string) (LLM, error) {
// §.splinter/page/pkg/llm/provider/New.fs
}
