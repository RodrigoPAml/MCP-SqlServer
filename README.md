# MCP-SqlServer

A MCP Server (STDIO) implementation in C# for LLM integration with SQL Server tested with Claude Desktop

The server register logs into a **mcp_server.log** file

**Caution, the LLM can modify your database**

# Tools

List of tools provided to the LLM

* **HealthCheck**: Tests if the Microsoft SQL Server Database connection is good and alive.
* **GetSchema**: Get a list of all tables with their respective schema, columns and types.
* **Query**: Execute a query into the Microsoft SQL Server database and return the result as a JSON.

# Setup with Claude

1. Fill the _env.database_ with the connection string from your SQL Server
2. Compile the project
3. In the Claude Destkop Go _File->Configurations->Developer_ and edit the MCP configuration (_claude_desktop_config.json_)
4. Paste the json as: 

```json
{
    "mcpServers": {
        "database": {
            "command": "dotnet",
            "args": [
                "run",
                "--project",
                "C:\\Users\\{USER}\\Desktop\\MCP-SqlServer",
                "--no-build"
            ]
        }
    }
}
```
5. Restart Claude Desktop
   
# Testing

Open Claude Desktop and if everything is configured well you should be able to see this

![image](https://github.com/user-attachments/assets/005590e3-e419-41e4-a3ae-9fec08d6ad7a)

Click on the icon and you should be able to see the tools

![image](https://github.com/user-attachments/assets/af776f81-8e61-46c3-9d9a-24ef08f8d813)

## Asking for connection check

![image](https://github.com/user-attachments/assets/086c2054-a88a-404b-a00c-a26751e1e7e0)

## Asking for schema/table informations

![image](https://github.com/user-attachments/assets/817dd04c-d530-40fe-8261-fe574ee674b2)

## Asking for read query

![image](https://github.com/user-attachments/assets/727b56e9-cee3-44f5-a4b6-33019771c43a)

## Asking for modifications

![image](https://github.com/user-attachments/assets/1af29d63-547f-4ad2-8a37-2eaae039c99b)
