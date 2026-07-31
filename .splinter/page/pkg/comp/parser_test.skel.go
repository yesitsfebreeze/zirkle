// §source page/pkg/comp/parser_test.go
package comp

import (
	"testing"
)

const testShard = `---
name: check-ci
description: Check CI pipeline status
kind: tool
tags: [ci, check]
use_when: CI build is failing
not_when: no CI configured
danger: none
side_effects: false
requires: [gh, jq]
run: check-ci
---

Check CI pipeline for failures. Reads status from GitHub Actions.

` + "```just\n[unix]\ncheck-ci:\n    gh run list --limit 5 --json conclusion --jq '.[].conclusion'\n```"

func TestSplitFrontmatter(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestSplitFrontmatter.fs
}

func TestSplitFrontmatterNone(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestSplitFrontmatterNone.fs
}

func TestParse(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestParse.fs
}

func TestParseNoFrontmatter(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestParseNoFrontmatter.fs
}

func TestScanLinks(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestScanLinks.fs
}

func TestScanLinksNone(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestScanLinksNone.fs
}

func TestExtractJustBlock(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestExtractJustBlock.fs
}

func TestExtractJustBlockNone(t *testing.T) {
// §.splinter/page/pkg/comp/parser_test/TestExtractJustBlockNone.fs
}

func contains(s, sub string) bool {
// §.splinter/page/pkg/comp/parser_test/contains.fs
}

func containsStr(s, sub string) bool {
// §.splinter/page/pkg/comp/parser_test/containsStr.fs
}
