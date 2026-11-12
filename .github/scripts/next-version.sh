#!/usr/bin/env bash
# Computes the next package version.
#
# VERSION holds only MAJOR.MINOR (for example "2.0"). The patch number is derived from the stable release tags
# (vMAJOR.MINOR.PATCH, no suffix): the highest existing patch + 1, or 0 when that line has no release yet.
# Pre-release tags such as v2.0.3-nightly.202611121200 are ignored.
#
# Prints KEY=VALUE lines, so a workflow can append the output straight to $GITHUB_OUTPUT:
#   line=2.0   patch=3   version=2.0.3   previous=v2.0.2   (previous = latest stable tag overall, may be empty)
set -euo pipefail

root="$(git rev-parse --show-toplevel)"
raw="$(tr -d '[:space:]' < "$root/VERSION")"

if ! [[ "$raw" =~ ^([0-9]+)\.([0-9]+)$ ]]; then
  echo "::error file=VERSION::'$raw' is not MAJOR.MINOR (for example 2.0). The patch number is computed from the release tags." >&2
  exit 1
fi
major="${BASH_REMATCH[1]}"
minor="${BASH_REMATCH[2]}"

latest_patch="$(git tag --list "v${major}.${minor}.*" | sed -nE "s/^v${major}\.${minor}\.([0-9]+)$/\1/p" | sort -n | tail -n 1)"
if [ -z "$latest_patch" ]; then patch=0; else patch=$((latest_patch + 1)); fi

previous="$(git tag --list 'v[0-9]*' | grep -E '^v[0-9]+\.[0-9]+\.[0-9]+$' | sort -V | tail -n 1 || true)"

echo "line=${major}.${minor}"
echo "patch=${patch}"
echo "version=${major}.${minor}.${patch}"
echo "previous=${previous}"
