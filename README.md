[![GitHub stars](https://img.shields.io/github/stars/MrBreakNFix/process-elevation?style=social)](https://github.com/MrBreakNFix/process-elevation/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/MrBreakNFix/process-elevation?style=social)](https://github.com/MrBreakNFix/process-elevation/network)
[![GitHub issues](https://img.shields.io/github/issues/MrBreakNFix/process-elevation)](https://github.com/MrBreakNFix/process-elevation/issues)


# Process Elevation

**Process Elevation** is a utility that allows you to run commands as a predefined user by communicating via a service worker using a NamedPipeServerStream. This repository is designed to work in conjunction with the Process Elevation Helper repository, enabling secure and controlled execution of commands with elevated privileges.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [License](#license)

## Introduction

This tool is used execute specific commands or operations with elevated privileges or another user account. Use https://github.com/MrBreakNFix/process-elevation-helper to communicate with elevsw.exe to launch programs from a different user account.

## Features

- **Predefined User:** You can specify a predefined user with the necessary privileges to run the commands

- **UAC Prompt Bypass:** Because of using the functionality of runas savecreds, it bypasses UAC prompts.

## Prerequisites

Before using Process Elevation, make sure you have the following prerequisites in place:

- .NET Core

- [Process Elevation Helper](https://github.com/MrBreakNFix/process-elevation-helper)

## Installation

1. Clone this repository to your local machine or download the executable from the releases.
2. Add the file to startup (not yet implemented)

## Usage

1. Run `elevsw.exe`, which will ask you for a username and password for the user you want to use. It should not ask again after the first time.

2. To communicate with the Process Elevation Helper, `elevsw.exe` should be running. The window hides itself when not needed and will show itself on error or first launch.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
