# RegFreeOOPDemo

Demo of using app manifests to allow `IUnknown`-based COM objects to be used without registration.

## Branches

- `master`<br/>The working example with embedded manifests on the client and server.  Free threaded.
- `actctx`<br/>The working-ish example with an embedded manifest on the server, and an activation context on the client.  Free threaded.
- `STA_broken`<br/>The broken example with an embedded manifest on the server, and an activation context on the client.  Apartment threaded.