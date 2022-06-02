# MusicApi
A MusicApi Web API application built using the .NET 6 Web API framework and MongoDB that aggregates music data using the Audio Network API.

## Architecture Diagram

![MusicApi Architecture Diagram](https://user-images.githubusercontent.com/94787187/171513277-95abba35-9cc4-4a3d-8464-8efcd22dcd96.png)

## Considerations and Assumptions

+ The application is built using a persistance layer (MongoDB) for a fast response time. Another approach can include Redis for caching or persistance.
+ The application only loads 500 music tracks and returns only a subset of the Track record to keep things simple and small. However, the application is built to be scalable.
+ The application seeds and updates the Database on Application startup. This is designed to keep it simple and easy to run. The assumption is that on production there will be a seperate scheduled or cronjob service to keep the Database updated. 
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
9. The application will be taking requests at `http://localhost:5000`. _(Ignore the `Now listening on: http://[::]:80` in the logs)_


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

### Visual Studio (or VSCode)
1. Install Visual Studio (or VSCode if preferred) from [here](https://visualstudio.microsoft.com/downloads/).
2. Navigate to the directory where you cloned the repo earlier and open the Solution from the `MusicApi.sln` file in the root of the directory.
3. Follow the previous steps to add secrets and Make sure MongoDB is set up and running.
4. Hit the Run icon and the project should build and launch in a browser.
5. Take a note of the url and port when the browser launches to test the endpoint. For example, `http://localhost:5000/`

_Note: You will have to Install the C# extension and/or the .NET SDK, if you are planning to use VSCode. It should automatically prompt you to install when you open the project for the first time._

### dotnet CLI
1. Install .NET SDK (CLI tools are bundled) if you haven't already. Find instructions [here](https://dotnet.microsoft.com/download). If you installed Visual Studio in the previous step, .NET CLI should already be installed.
2. Open your preferred terminal and Navigate to the directory where you cloned the repo earlier. For example `cd ~/repos/MusicApi/MusicApi`
3. Build the project using `dotnet build` command. Make sure the Build succeeds at this stage
4. Run the 'MusicApi' project using `dotnet run --project MusicApi`. Make note of the url and port from the log `Now listening on: http://localhost:5000`

## Testing endpoints
Run the project using any of the methods above, and then use your preferred API client (Postman, httpie, curl or even your browser!) to test these endpoints. Make sure the the correct port is used when querying the endpoints. Replace the port `5000` with your own.

+ GET `http://localhost:5000/swagger` : Autogenerated OpenAPI swagger documentation. You can also use this to try the endpoints out. Click the appropriate endpoint and click the `Try it out` button. Input a word in the parameters box and click the `Execute` button to initiate an API call.
+ GET `http://localhost:5000/health` : Basic healthcheck
+ GET `http://localhost:5000/api/tracks` : Returns a list of all tracks
  + Example call using httpie `http localhost:5000/api/tracks`
  + Response would be:
  +  _(Rest of result omitted)_
  ```json
  [
    {
        "album": {
            "description": "From the former in-house producer at Dr Dre’s Aftermath comes an upbeat yet darkly hypnotic set exploring EDM-influenced trap. Features modulated vocals samples, hooky synth melodies, layered FX, dark clap loops, bouncy beats and melodic 808 bass.",
            "mainMixTrackCount": 10,
            "name": "Some Ting",
            "number": 3720,
            "pillar": "Core",
            "trackCount": 86
        },
        "description": "Dark, hypnotic trap with pitched vocal loop & vocal shouts, heavy bass, synths & driving trap beats",
        "explicit": false,
        "id": 1059108,
        "keywords": "Mell Beets ; beats ; beats series ; modulator ; modulated ; loop ; sample ; hook ; hypnotic ; driving ; forward motion ; energy ; energetic ; synths ; shouts ; vocal shouts ; ",
        "releaseDate": "2022-05-31T00:00:00Z",
        "title": "Loosie"
    }, ...  
  ```
+ GET `http://localhost:5000/api/tracks/{id}` : Returns a track with given id
  + Example call using httpie `http localhost:5000/api/tracks/1057691`
  + Response would be:
  ```json
  {
    "album": {
        "description": "Pioneering ambient composer Theo Travis (Robert Fripp, Harold Budd) and producer Paul Ressel (Lana Del Rey) explore ominous soundscapes. Textural arrangements include unnerving saxophones, clarinet, alto flute, deep drone bass, synth glitches and guitar.",
        "mainMixTrackCount": 6,
        "name": "Beauty In Hell",
        "number": 3715,
        "pillar": "Core",
        "trackCount": 85
    },
    "description": "Slow moody soundscape with moody clarinets, deep drone, synths & subtle acoustic guitar textures",
    "explicit": false,
    "id": 1057691,
    "keywords": "desolate ; clarinets ; hollow ; empty ; nothingness ; unsettled ; developing ; synth ; synthesizer ; deep drone ; developing ; synth bass",
    "releaseDate": "2022-05-30T00:00:00Z",
    "title": "Heart Of The Storm 10"
  }
  ```
  
+ GET `http://localhost:5000/api/tracks/count/{word}` : For a given word, will return the count of tracks where given word is included in the title
  + Example call using httpie `http localhost:5000/api/tracks/count/heart`
  + Response would be:
  ```json
  {
    "count": 11
  }
  ```
  
+ GET `http://localhost:5000/api/tracks/search/{word}` : For a given word, will return the list of tracks where given word is included in the title
  + Example call using httpie `http localhost:5000/api/tracks/count/heart`
  + Response would be:
  +  _(Rest of result omitted)_
  ```json
  [
    {
        "album": {
            "description": "Pioneering ambient composer Theo Travis (Robert Fripp, Harold Budd) and producer Paul Ressel (Lana Del Rey) explore ominous soundscapes. Textural arrangements include unnerving saxophones, clarinet, alto flute, deep drone bass, synth glitches and guitar.",
            "mainMixTrackCount": 6,
            "name": "Beauty In Hell",
            "number": 3715,
            "pillar": "Core",
            "trackCount": 85
        },
        "description": "Slow moody soundscape with moody clarinets, deep drone, synths & subtle acoustic guitar textures",
        "explicit": false,
        "id": 1055576,
        "keywords": "desolate ; clarinets ; hollow ; empty ; nothingness ; unsettled ; developing ; synth ; synthesizer ; deep drone ; developing ; synth bass",
        "releaseDate": "2022-05-30T00:00:00Z",
        "title": "Heart Of The Storm"
    },
    {
        "album": {
            "description": "Pioneering ambient composer Theo Travis (Robert Fripp, Harold Budd) and producer Paul Ressel (Lana Del Rey) explore ominous soundscapes. Textural arrangements include unnerving saxophones, clarinet, alto flute, deep drone bass, synth glitches and guitar.",
            "mainMixTrackCount": 6,
            "name": "Beauty In Hell",
            "number": 3715,
            "pillar": "Core",
            "trackCount": 85
        },
        "description": "Slow moody soundscape with deep drone, synths & acoustic guitar textures. No clarinets",
        "explicit": false,
        "id": 1057286,
        "keywords": "desolate ; clarinets ; hollow ; empty ; nothingness ; unsettled ; developing ; synth ; synthesizer ; deep drone ; developing ;",
        "releaseDate": "2022-05-30T00:00:00Z",
        "title": "Heart Of The Storm 2"
    }, ...
  ```
  
## Running Tests

The project consists of a number of Unit Tests. Unit tests test methods in isolation of dependencies.

### Using Visual Studio (or VSCode)
![Running Tests in Visual Studio](https://user-images.githubusercontent.com/94787187/171515789-f285677e-8ab6-4e3c-aa94-8bbf82d21daf.png)

1. Open the solution in Visual Studio
2. Open the Test Explorer window/tab and hit the Run Tests button

### Using dotnet CLI
1. Make sure .NET CLI is installed from the previous steps
2. Open your preferred terminal and Navigate to the directory where you cloned the repo earlier. For example `cd ~/repos/MusicApi/MusicApi.Tests`
3. Run the command `dotnet test`
