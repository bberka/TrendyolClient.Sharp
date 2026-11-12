#!/usr/bin/env bash
# Fails when a Markdown file has an unclosed code fence, which silently swallows the rest of the page.
fail=0
for f in "$@"; do
  [ -f "$f" ] || continue
  fences="$(grep -cE '^[[:space:]]*```' "$f")"
  if [ $((fences % 2)) -ne 0 ]; then
    echo "$f: $fences code fence lines; one is not closed" >&2
    fail=1
  fi
done
exit "$fail"
