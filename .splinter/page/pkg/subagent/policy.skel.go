// §source page/pkg/subagent/policy.go
package subagent

import (
	"fmt"
	"net"
	"os"
	"sync"

	"github.com/feb/relay/pkg/comp"
	"github.com/feb/relay/pkg/egress"
	"github.com/feb/relay/pkg/sandbox"
)

// EnvSandbox switches the default executor.  Set it to off/0/false/no to run
// subagents unconfined on this machine.
const EnvSandbox = "RELAY_SANDBOX"

// forwardedEnv are the host variables a sandboxed subagent needs to reach a
// model.  The sandbox clears the environment, so anything not listed here is
// simply absent inside — which is the point: the list is the leak surface.
var forwardedEnv = []string{
	"RELAY_LLM_PROVIDER",
	"RELAY_MODEL",
	"OLLAMA_HOST",
	"ANTHROPIC_API_KEY",
}

var warnUnconfined sync.Once

// DefaultSpec is the sandbox a subagent gets when nobody names one: a
// RAM-backed root that dies with the process, since a subagent returns a
// Result rather than files.  Net is off and an egress policy allows only the
// Ollama endpoint, so the subagent cannot reach arbitrary hosts.
func DefaultSpec() sandbox.Spec {
// §.splinter/page/pkg/subagent/policy/DefaultSpec.fs
}

// Unconfined reports whether EnvSandbox asks for the local escape hatch.
func Unconfined() bool {
// §.splinter/page/pkg/subagent/policy/Unconfined.fs
}

// DefaultExecutor is the policy behind a nil Config.Executor: confined unless
// told otherwise.  A host that cannot sandbox is an error, never a silent
// downgrade — the whole guarantee is that a pod runs confined, so failing
// loudly is the only honest answer.
func DefaultExecutor() (Executor, error) {
// §.splinter/page/pkg/subagent/policy/DefaultExecutor.fs
}
