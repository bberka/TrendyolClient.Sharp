#!/usr/bin/env bash
# Pre-commit check: build everything with warnings as errors (Directory.Build.props), then run the tests.
# The staged file list arrives as arguments only so the Husky task can be skipped when no code changed; it is not used here.
set -e
dotnet build --configuration Release --nologo --verbosity quiet
dotnet test --configuration Release --no-build --nologo --verbosity quiet
