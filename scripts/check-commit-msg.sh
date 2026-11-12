#!/usr/bin/env bash
# Checks the shape of a commit message (run by the commit-msg Husky hook with the message file as $1):
# a non-empty subject of at most 72 characters without a trailing period, a blank second line,
# and a body wrapped at 72 characters. Merge, revert, fixup and squash commits are skipped.
file="$1"
subject="$(head -n 1 "$file")"

case "$subject" in
  Merge*|Revert*|fixup!*|squash!*) exit 0 ;;
esac

fail=0
err() { echo "commit message: $1" >&2; fail=1; }

[ -n "$subject" ] || err "the subject line is empty"
[ "${#subject}" -le 72 ] || err "the subject is ${#subject} characters; keep it to 72"
[[ "$subject" != *. ]] || err "the subject ends with a period"
[ -z "$(sed -n 2p "$file")" ] || err "leave a blank line after the subject"

n=0
while IFS= read -r line; do
  n=$((n + 1))
  [ "$n" -le 2 ] && continue
  case "$line" in '#'*) continue ;; esac   # comment lines from the editor
  case "$line" in *http*) continue ;; esac  # long URLs cannot be wrapped
  [ "${#line}" -le 72 ] || err "line $n is ${#line} characters; wrap the body at 72"
done < "$file"

exit "$fail"
