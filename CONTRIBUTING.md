# Contributing to Chia API Library
Thank you for considering contributing to the Chia API Library! We appreciate your help in making this project better.

This library is an open-source API library for the Chia blockchain client, written in C#. It provides a convenient way for developers to interact with the Chia blockchain and build applications on top of it.

## Code of Conduct
We follow the Contributor Covenant Code of Conduct. By participating in this project, you are expected to uphold this code.

## Getting Started
1. Fork the repository
2. Clone the repository to your local machine
3. Create a new branch for your changes
4. Make the changes and commit them to your branch
5. Push the changes to your forked repository
6. Create a pull request to the main repository

## Variable Naming Conventions
- names are to be put in PascalCase. 
  If there are multiple variables or names for the same topic, the naming should follow Name_Topic such as `GetCoinRecordByName_RPC` & `GetCoinRecordByName_Response`.
- variables for json serialisation & deserialisation follow the naming convention of the json file. If the variables there are names in snake_case, the serialisation classes use snake_case

## Code Style
- Follow the .NET coding style guidelines.
- Use the built-in libraries of .NET whenever possible, instead of relying on third-party libraries.
- Write well-documented code, with clear and concise comments explaining the purpose and functionality of the code.

## Testing
- Make sure to write tests for any new features or changes.
- Tests should be written using the XUNIT Project.

## Pull Request Process
- Your pull request will be reviewed by one of the maintainers.
- If any changes are required, the maintainer will request them.
- Once the changes have been made and the pull request has been updated, the pull request will be merged.

# Thank You!
Thank you for your contribution to the Chia API Library! We appreciate your help in making this project better.
