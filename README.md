# gh

# Setup guide
    1. Make sure to update your local.settings.json file with your github token (sample below) 
    ```json
    {
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "GH_TOKEN": "<github_token>",
        "GH_BASE_URL": "https://api.github.com/"
    }
    }
    ``` 

    2. Run the project

    3. To see the swagger view, once the project is running you can below url
        # Local host swagger ui http://localhost:PORT/api/swagger/ui

    4. Make the requests!

# Architecture High Level Diagram
insert diagram here....


# Architecture High Level Design
    Utilized a serverless architecture, Azure functions, to accomplish this task.
    If we are to deploy this Azure this is how the overall design will be.
    1. Deploy functions to Azure via ci/cd pipeline
    2. Add the secrets in the app settings of the function so it will be able to grab the token, connection string, etc...
    3. Deploy Azure Api Management (APIM) and stitch the function to the APIM via the swagger 
    import feature (We accomplish this by call the swagger endpoint from the function)
    4. Within APIM you can add policies like validating JWT tokens, caching, rate limiting, etc...
    Thats pretty much it.... if you want an E2E solution
    # As a bonus you can add Azure Front Door (similiar to cloud front from aws) on top 
    of APIM or directly on top on the functions.
    Considerations for Azure Front Door, 
    # Can support Geo disater recovery (Primary & Secondary region) and route traffic accordingly
    # Can support multiple policies (ex: sql injection prevetion, rate limiting, etc...)

# Reason for implementing a serveless approach
    # Cost effective 
        # At least in Azure, its basically free
            # Functions are free up till 1 million executions
            # Apim consumption plan is free with 99% reliability
    # Highly Scaleable (Scale with need)
        # Functions will automatically scale based on consumption
    # No infrastructure to manage (everything is managed by cloud provider)
    # The ask for this task is fairly simple, it is not complex, there is no need to 
    create .net core web api and deploy to AKS (aws EKS equivalient) or 
    app services (aws Fargate equivalent), going down that route will 
    significantly increase cost,complexity and added maintence especially if you 
    decided to deploy to AKS (aws EKS equivalient).
    # Can easily implement timed functions if you want it to run on a CRON
    # Can easily trigger the function via http trigger, blob trigger, service bus queue trigger, etc... 

# Code design decisions
    # I implemented there two approaches (Service Pattern and CQS)
    # GhRepos.cs follows the Service Pattern 
        # In this case, I just injected the Proxy directly, but the idea is still the same 
        # Pros:
            # It's a common pattern
        # Cons:
            # Violates Single responsibilty principal
            # End up with MONSTER classes with thousands of lines
            # Difficult to test, read/follow, etc...
            # Too many things happining 
    # GhReposWithMediator.cs follows the Command Query Seperation (CQS) pattern with Meditor
        # Pros:
            # Like how .net works behind the scenes, Meditor follows that pipeline approach. 
            The benefit of this allows you segment your code and apply seperation of concerns. 
            # Your classes become much smaller and readable.
            # Follows Single responsibilty principal 
            # If you are approaching the task form a domain perspective (DDD) then you Commands and Queries will be organized by Domain
            # Easier to test
            # Classes are not polluted 
            # etc.......
        # Cons:
            # Could add an extra layer of complexity, but its 100% worth implementing this, will make your lives easier, trust me
        # Example mediator flow:
            # request -> (can add logging) -> (can add caching) -> execute the CQS handlers (aka your busniess logic) -> (can add caching) -> (can add logging) -> response
        

    # (Request Response pattern) Added Mapster to map To and From domain model To Request/Response models.
        # You never want to expose your internal schema/model and want to restrict the request/repsonse for the user. Also you want to control what the user sends and what they get back

# Future Enchancements
    # Request Validation
        # Can be accomplished with Mediator validation behavior
    # Middleware for jwt authentication to protect the function endpoints
    # Consolidate logging and handle expections better (custom exceptions)

