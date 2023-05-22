# ScriptBloxAPI
### GetScriptFromScriptbloxId

Retrieves a script from Scriptblox based on the provided Scriptblox ID.

**Parameters:**
- `bloxId` (string): The Scriptblox ID of the script to retrieve.

**Returns:**
- `Script`: The script retrieved from the API, or a default script if the retrieval fails or the data is invalid.

### GetFrontPageScripts

Retrieves a list of scripts from the front page based on the provided page number.

**Parameters:**
- `pageNumber` (int, optional): The page number of the front page scripts (default is 1).

**Returns:**
- `List<Script>`: A list of Script objects representing the scripts from the front page.

### GetScriptsFromPageNumber

Retrieves a list of scripts from the front page based on the provided page number.

**Parameters:**
- `pageNumber` (int, optional): The page number of the front page scripts (default is 1).

**Returns:**
- `List<Script>`: A list of Script objects representing the scripts from the front page.

### GetScriptsFromQuery

Retrieves a list of scripts from Scriptblox based on the provided search query and maximum results.

**Parameters:**
- `searchQuery` (string): The search query to filter the scripts.
- `maxResults` (int, optional): The maximum number of results to retrieve (default is 20).

**Returns:**
- `List<Script>`: A list of Script objects representing the scripts matching the search query.

### GetScriptsFromUser

Retrieves a list of scripts from Scriptblox based on the provided username.

**Parameters:**
- `username` (string): The username of the user whose scripts to retrieve.

**Returns:**
- `List<Script>`: A list of Script objects representing the scripts owned by the user.

Note: The `GetScriptsFromPageNumber` function is an alias for `GetFrontPageScripts` and behaves the same way.
