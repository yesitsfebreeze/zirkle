// §source page/pkg/adapter/adapter.go
// Package adapter defines the single channel seam between the daemon and the
// outside world. A channel (Slack, webhook, CLI, cron, TUI, voice, ...) is just
// an adapter: it implements InputAdapter, OutputAdapter, or both. There is no
// special-cased integration — Slack is one adapter among many, not a named
// channel kind.
//
// Modalities a channel may carry: text or voice on input (Prompt is text;
// voice arrives when a voice adapter transcribes or forwards raw audio); text,
// image, or TTS audio on output. Today only text is wired; the seam stays
// text-shaped until a real non-text adapter lands, at which point the structs
// grow the matching optional fields rather than the transports being special.
package adapter

import "context"

// InMessage is a trigger fed into the daemon to drive an agent.
type InMessage struct {
	Source    string         // "slack" | "webhook" | "cli" | "cron" | ...
	ChannelID string         // conversation handle: thread, secret, argv, ...
	Prompt    string         // text handed to the agent
	Meta      map[string]any // idempotency key, user id, raw body, ...
}

// OutMessage is an agent's response routed back out through a channel.
type OutMessage struct {
	ChannelID string // routes to the originating conversation
	Text      string
}

// InputAdapter sources inbound triggers. Run blocks until ctx is cancelled,
// invoking deliver for each event. The adapter owns its event loop
// (HTTP server, Socket Mode client, cron ticker, ...) and reconnect logic.
type InputAdapter interface {
	Run(ctx context.Context, deliver func(InMessage)) error
}

// OutputAdapter sinks agent output to a channel. Implementations: Slack
// chat.postMessage, TUI render, stdout. A channel that can render images or
// TTS does so from the same OutMessage; the seam does not favour text.
type OutputAdapter interface {
	Send(ctx context.Context, out OutMessage) error
}
