// §source page/pkg/subagent/fuzz_test.go
package subagent

import "testing"

// decodeResult parses JSON written by a separate process over fd 3. That
// process can crash mid-write, emit a banner, or be an entirely different
// binary, so the bytes are untrusted: a malformed frame must be an error,
// never a panic.
func FuzzDecodeResult(f *testing.F) {
// §.splinter/page/pkg/subagent/fuzz_test/FuzzDecodeResult.fs
}
