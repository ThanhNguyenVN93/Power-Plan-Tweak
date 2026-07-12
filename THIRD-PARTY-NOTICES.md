# Third-party notices

This project's own source code is licensed under the MIT License (see
[LICENSE](LICENSE)). It depends on the following third-party components,
which are **not** covered by that MIT license and are **not** included in
this repository:

## Guna.UI2.WinForms

Used for the UI controls (`Guna2Button`, `Guna2TextBox`, `Guna2Panel`, etc.).

- **License**: commercial, per-developer EULA (paid activation code, 14-day
  free trial). See the vendor's terms at <https://github.com/Saleh-Kh/Guna.UI2.WinForms>
  or wherever you obtain the package — do not assume MIT/free terms apply to it.
- **Not committed to this repo** for that reason. `packages/` is gitignored.
- Compiled applications built with it can be freely distributed royalty-free
  per its EULA — the restriction is on redistributing the SDK/DLL itself, not
  on the exe you build with it.
- To build this project yourself, restore `Guna.UI2.WinForms` version `2.0.4.8`
  via NuGet (`packages.config`-based restore, e.g. through Visual Studio's
  "Restore NuGet Packages") using your own license/trial.

## ILRepack

Used only as a build-time tool (`tools/ILRepack/bin/ILRepack.exe`) to merge
`Guna.UI2.dll` into the Release exe so distribution is a single file.

- **License**: Apache License 2.0.
- **Included in this repo** (small, freely redistributable) so Release builds
  work out of the box. Source: <https://github.com/gluck/il-repack>.
