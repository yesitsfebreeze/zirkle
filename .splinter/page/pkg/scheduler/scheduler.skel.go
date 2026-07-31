// §source page/pkg/scheduler/scheduler.go
// Package scheduler fires registered agents on cron schedules.
//
// It wraps robfig/cron/v3 and is deliberately decoupled from pkg/agent and
// pkg/store: the daemon supplies a Trigger adapter that resumes one agent
// turn per fire. scheduler owns only cron parsing, ticking, and dispatch.
package scheduler

import (
	"context"
	"fmt"

	"github.com/robfig/cron/v3"
)

// Trigger resumes a single scheduled turn for the named agent. Implementations
// run the agent one turn, checkpoint, and return. Errors are swallowed by the
// cron loop (there is no error channel); the daemon-side Trigger is expected
// to log them itself.
type Trigger interface {
	Fire(ctx context.Context, agentID string) error
}

// TriggerFunc adapts a plain function to Trigger.
type TriggerFunc func(ctx context.Context, agentID string) error

func (f TriggerFunc) Fire(ctx context.Context, agentID string) error {
// §.splinter/page/pkg/scheduler/scheduler/TriggerFunc.Fire.fs
}

// Option is forwarded to robfig/cron. Use cron.WithSeconds for 6-field
// expressions (leading seconds field) and cron.WithLocation for a non-UTC TZ.
type Option = cron.Option

// Scheduler wraps a robfig/cron instance.
type Scheduler struct {
	cron *cron.Cron
}

// New builds a Scheduler. Pass cron options (e.g. cron.WithSeconds()).
func New(opts ...Option) *Scheduler {
// §.splinter/page/pkg/scheduler/scheduler/New.fs
}

// Add registers t to fire for agentID on the cron schedule spec (5-field by
// default; 6-field if cron.WithSeconds() was passed to New). Returns an error
// if the spec is not a valid cron expression or t is nil.
func (s *Scheduler) Add(spec string, agentID string, t Trigger) error {
// §.splinter/page/pkg/scheduler/scheduler/Scheduler.Add.fs
}

// Start runs the cron loop in a background goroutine.
func (s *Scheduler) Start() {
// §.splinter/page/pkg/scheduler/scheduler/Scheduler.Start.fs
}

// Stop halts scheduling. The returned context is done once all in-flight jobs
// have finished, mirroring robfig/cron semantics.
func (s *Scheduler) Stop() context.Context {
// §.splinter/page/pkg/scheduler/scheduler/Scheduler.Stop.fs
}

// cronJob adapts a Trigger to the cron.Job interface (Run() with no args).
type cronJob struct {
	agentID string
	t       Trigger
}

func (j cronJob) Run() {
// §.splinter/page/pkg/scheduler/scheduler/cronJob.Run.fs
}
