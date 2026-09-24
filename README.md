# UnityGit

Pure C# Git client library for Unity, packaged as a Unity Package Manager package. Add this repository through Package Manager using its Git URL.

Initial implementation includes Git SHA-1 object hashing, Git-compatible loose-object zlib storage, Blob/Tree/Commit/Tag object serialization, a v2 index reader/writer, repository initialization/opening, branch name resolution, file staging/status, commit creation, and commit log traversal. An Editor window is available at Window > UnityGit.

Tests are NUnit-based Unity Editor tests covering known SHA-1, loose object round trips, and commit/log creation. The implementation targets modern Unity APIs; the Editor test suite should be run in Unity 2021.3 or later.

Limitations: this is an initial, intentionally scoped implementation. It does not yet implement full Git interoperability (for example packed objects, merge/index extensions, rename detection, robust worktree traversal, and complete diff/inspect). The binary index parser handles v2 entries only.
