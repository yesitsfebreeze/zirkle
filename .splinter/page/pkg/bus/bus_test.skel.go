// §source page/pkg/bus/bus_test.go
package bus

import (
	"os"
	"path/filepath"
	"testing"
)

func TestGenerateIdentity(t *testing.T) {
// §.splinter/page/pkg/bus/bus_test/TestGenerateIdentity.fs
}

func TestSignVerifyRoundtrip(t *testing.T) {
// §.splinter/page/pkg/bus/bus_test/TestSignVerifyRoundtrip.fs
}

func TestEnvelopeSignVerify(t *testing.T) {
// §.splinter/page/pkg/bus/bus_test/TestEnvelopeSignVerify.fs
}

func TestSendPollAckRoundtrip(t *testing.T) {
// §.splinter/page/pkg/bus/bus_test/TestSendPollAckRoundtrip.fs
}

func TestPollRejectsUnsigned(t *testing.T) {
// §.splinter/page/pkg/bus/bus_test/TestPollRejectsUnsigned.fs
}

func TestPollNonBlockingEmpty(t *testing.T) {
// §.splinter/page/pkg/bus/bus_test/TestPollNonBlockingEmpty.fs
}
