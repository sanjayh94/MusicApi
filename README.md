# MusicApi
A MusicApi Web API application built using the .NET 6 Web API framework and MongoDB that retrieves a list of tracks for a given word using the Audio Network API.

## Architecture Diagram

![MusicApi Architecture Diagram](https://user-images.githubusercontent.com/94787187/171513277-95abba35-9cc4-4a3d-8464-8efcd22dcd96.png)

## Considerations and Assumptions

+ The application is built using a persistance layer (MongoDB) for a fast response time. Another approach can include Redis for caching or persistance.
+ The application only loads 500 music tracks and returns only a subset of the Track record to keep things simple and small. However, the application is built to be scalable.
+ The application seeds the Database on Application startup. This is designed to keep it simple and easy to run. The assumption is that on production there will be a seperate scheduled or cronjob service to keep the Database updated. 
+ The overall goal was to keep it simple, but feel free to ask questions regarding the application architecture or approach.

## How to Run
I personally used Visual Studio to develop, test and run the project, but there are multiple ways to run this project. The different methods are listed below.

**First, clone the repo in to the desired folder using CLI `git clone https://github.com/sanjayh94/MusicApi/` or your favourite IDE/GUI tool.**

*You will need git CLI installed for this command to work, Find instructions [here](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)*

### Docker
1. Download and Install Docker and docker-compose if you haven't already. Find instructions [here](https://docs.docker.com/get-docker/). _Note: Verify Docker is running on Linux containers mode._
2. Open your preferred terminal and Navigate to the directory where you cloned the repo earlier. For example `cd ~/repos/MusicApi`
3. Navigate to the MusicApi Project folder `cd MusicApi`. Create an empty `secrets.json` file. `touch secrets.json`. _The secrets.json file should be inside the application folder, not the root of the repo. For example, `cd ~/repos/MusicApi/MusicApi/secrets.json`_
4. Add the Audio Network Public Api Key
```json
{
    "AudioNetworkApiKey": "YOUR_KEY_HERE"
}
```
5. Navigate to `appsettings.json` and verify the `MusicTracksDatabase.ConnectionString` is `"ConnectionString": "mongodb://musictracksdb:27017"`
```json
{
  "MusicTracksDatabase": {
    "ConnectionString": "mongodb://musictracksdb:27017",
    "DatabaseName": "MusicTracks",
    "TracksCollectionName": "Tracks"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
6. Navigate to the root of the repo `cd ~/repos/MusicApi`.
7. Build the images `docker-compose build`
8. Run the application `docker-compose up`
9. The application will be taking requests at `http://localhost:5000`


### Setting up MongoDB Server
*Not needed for the Docker approach, only for running a local server through IDE or CLI*
1. Download and Install MongoDB Server from [here](https://www.mongodb.com/try/download/community).
2. Choose a directory on your development machine for storing the data.
3. Open a command shell. Run the following command to connect to MongoDB on default port 27017. Remember to replace <data_directory_path> with the directory you chose in the previous step.
`mongod --dbpath <data_directory_path>`
4. In the application folder, make sure the file `MusicApi/appsettings.json` has the correct connection string `"ConnectionString": "mongodb://localhost:27017"`
```json
{
  "MusicTracksDatabase": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "MusicTracks",
    "TracksCollectionName": "Tracks"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
5. The Database should get seeded when the application starts up.

