# MCP-SqlServer

A MCP Server implementation in C# for LLM integration with SQL Server (STDIO) tested with Claude Desktop

The server register logs into a **mcp_server.log** file

**Caution, the LLM can modify your database**

# Tools

List of tools provided to the LLM

* **HealthCheck**: Tests if the Microsoft SQL Server Database connection is good and alive.
* **GetTablesWithColumnsAndTypes**: Get a list of all tables with their respective columns, including schemas.
* **Query**: Execute a query into the Microsoft SQL Server database and return the result as a JSON.

# Setup with Claude

1. Fill the _env.database_ with the connection string from your SQL Server
2. Compile the project
3. In the Claude Destkop Go _File->Configurations->Developer_ and edit the MCP configuration 
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

![image](https://github.com/user-attachments/assets/4cc9fcf2-944d-46f7-8f16-d46eaa1ed59a)

Click on the icon and you should be able to see the tools

![image](https://github.com/user-attachments/assets/bb5a7b83-5459-43d5-b7d0-9d5c5880ff5c)

## Asking for connection check

![image](https://github.com/user-attachments/assets/65f0edb8-25a4-4d36-84bd-a28f0a004e20)

## Asking for schema/table informations

![image](https://github.com/user-attachments/assets/834b4717-2f12-4926-853e-31973c25ad2f)

## Asking for read query

![image](https://github.com/user-attachments/assets/1dfa2186-4e6a-4a54-8aa6-d5f7b5ed7a92)

## Asking for modifications

![image](https://github.com/user-attachments/assets/57b49aa3-7592-436e-8f67-bff3f54911ae)
