# AGENTS.md

Instructions for AI coding agents working in this repository. Humans can follow them too.

## The project

TrendyolClient.Sharp is a type-safe C# client for the Trendyol APIs, built on Refit. It maps Trendyol's documented endpoints to typed methods and does nothing else: no input validation, no retries, no throttling. The README "Scope" section states this policy, and it stays.

```
src/TrendyolClient.Sharp/    the package
  ITrendyol*Api.cs           one Refit interface per API area (products, orders, claims, ...)
  Models/                    one class per file, named Trendyol*, TrendyolIntl* or TrendyolGlobal*
  Handlers/ Exceptions/ Enums/
  TrendyolMarketplaceClientFactory.cs, TrendyolServiceCollectionExtensions.cs, TrendyolJson.cs, TrendyolCredentials.cs
tests/TrendyolClient.Sharp.Tests/   route smoke tests, fixture round trips, setup and error tests
scripts/  .husky/                   Git hook checks
.github/workflows/                  ci.yml, nightly-release.yml, release.yml
VERSION                             MAJOR.MINOR only; CI computes the patch from release tags
```

Commands:

```bash
dotnet build -c Release     # warnings are errors (Directory.Build.props)
dotnet test -c Release      # net8.0, net9.0 and net10.0
```

## Workflow for every task

1. Use a worktree. Work in a separate worktree per task so tasks do not disturb each other:
   `git fetch origin && git worktree add ../TrendyolClient.Sharp-<topic> -b <type>/<topic> origin/master`.
   Never commit to `master`; branch protection blocks it anyway.
2. Run the checks before you commit. The Git hooks do it: they build with warnings as errors, run the tests, check that Markdown code fences are closed, and check the commit message shape. The first `dotnet restore` installs them. Do not bypass them with `--no-verify`; fix what they report. A docs-only commit skips the build and tests.
3. Commit in logical groups. One concern per commit, with its tests and docs. Subject in the imperative, at most 72 characters, no trailing period. Wrap the body at 72 and explain why, not what.
4. Open the PR as soon as the work is done. `git push -u origin <branch>` then `gh pr create --base master`. The body says why, what changed, what you verified, and what you could not verify. Do not wait to be asked.
5. Wait for CI. The required check is **Build & test**. Watch the run for your newest commit (`gh pr checks <n> --watch`), not an older one.
6. If CI fails, fix and wait again, up to 3 times. Read the log with `gh run view <id> --log-failed`, fix the cause, commit, push, and wait. If it fails for a reason your change did not cause (a runner outage, an expired secret, a network error, a NuGet or GitHub incident), stop and tell the user, with the run link. Do not retry blindly or work around it. After 3 failed attempts, stop and tell the user too.
7. **Do not merge unless the user asks.** When they do, merge only after CI passes. Merged branches are deleted automatically; remove your worktree afterwards.

## Rules for the code

- Warnings are errors. Fix the cause. Do not suppress a warning to get past it.
- The public API is frozen within a major version. Do not rename or remove public types, members, JSON names or enum members in a minor release. Put such ideas in the PR description instead.
- Models mirror Trendyol's JSON. One class per file, `[JsonPropertyName]` in camelCase on every property, no nullable annotations. Many shapes look alike but differ per endpoint or region; do not merge them without checking the wire format. Keep the XML summaries that carry Trendyol limits and region rules.
- Routes use the literal token `SELLER_ID_PARAMETER` where the seller id goes. The injector handler replaces it.
- The library has no validation, retries or throttling. Consumers add them through the `IHttpClientBuilder` that `AddTrendyolApiClient` returns.
- Sources: implement from developers.trendyol.com, not from memory. The portal has several doc sets: Türkiye (v1.0, v2.0 is its English copy), International Marketplace (v3.0), Global Partner (v5.0) and Export Center (v6.0). Each page is available as Markdown at `<url>.md`, and `/llms.txt` lists them. When pages disagree, trust the newest changelog entry, then the docs page, then the reference page, and say which you chose.
- Tests: a new endpoint needs a route (the smoke test covers every interface method automatically). A new model with a documented example payload needs a fixture in `tests/.../Fixtures` and a case in `FixtureCoverageTests`, which fails when a model drops a field.
- Do not use Trendyol's logo or colors. The project is unofficial.

## Documentation

- Update the docs in the same PR as the code: `README.md` (it is also the NuGet readme) and this file. Remove statements that stopped being true.
- Write plainly. No filler, no recaps, no inflated words, no em dashes. Run the `unslop-text` skill on every Markdown file you changed when it is available.
- `VERSION` changes only for a new minor or major version. The patch is computed.

## Releasing

Every push to `master` publishes a nightly pre-release. A stable release happens only when the user runs the **Release** workflow by hand. Publishing uses NuGet trusted publishing, so there is no API key to manage. Details are in the README "Releasing" section.

## Things that already went wrong

- Refit registers a global camelCase enum converter that beats a `[JsonConverter]` on the enum type. `TrendyolJson` inserts our converter first; build JSON options through it.
- The seller id travels in the internal `X-Internal-Seller-Id` header, which the primary handler removes after every other handler. Read it before the request is sent.
- Trendyol sends some numbers (cargo tracking numbers) larger than `long`. Use `TrendyolFlexibleStringConverter`.
- Example JSON in the docs contains comments and typos. Clean it before using it as a fixture.
- The release workflow once matched no files because a comment sat inside a multi-line `files:` value.
