# EasyTempFolder
----

EasyTempFolder is a class intended to make creation and deletion of temporary folders easier.

Usage is simple:

```csharp
using DanTheMan827.TempFolders;

using (var temp = new EasyTempFolder())
{
	// do something with the temp folder
}
```

Upon calling `Dispose()`, the temporary folder will be automatically delete.

If you don't want this behavior, you can specify `deleteAfter: false` in the class initialization.

## License

Copyright 2022 Daniel Radtke

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.