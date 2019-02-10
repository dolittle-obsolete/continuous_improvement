# Build Log Handler

The purpose of this project is to provide a way to easily handle logs coming during builds.
It leverages pipes and takes any STDIN and parses what it gets. It looks at what it gets
and tries to parse it to make sense, based on the type of step being built.
Raw logs are passed through to a log file and any recognized results, such as compiler
warning / errors / info is put into a structured JSON output file.

## Building

You'll need to have GCC installed to compile this. This particular software has not been
tested to compile on Windows. Unix based systems are known to work.

For Linux users, please go [here](https://gcc.gnu.org/install/) for a guideline on what to install for GCC.

macOS users can install it through XCode - open a shell:

```shell
$ xcode-select --install
```

Once you have this installed you can reopen a fresh terminal and just run `make` from this folder.

## VSCode

If you're using VSCode and you want to get some intellisense and better experience, recommend installing
[this](https://marketplace.visualstudio.com/items?itemName=ms-vscode.cpptools) and [this](https://marketplace.visualstudio.com/items?itemName=ACharLuk.easy-cpp-projects).
The latter makes it simpler to create new projects - this project was created with it.