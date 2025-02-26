# Weather App

## Overview
This is a .NET-based weather application that provides real-time weather information. The app retrieves weather data using an external API and visually represents different weather conditions. There is no images or icons in this app, evertying you see, including icons
and animations is generated through code.

## Features
- Real-time weather updates
- Icon animations
- Supports different weather conditions (drizzle, sleet, blizzards, etc.)
- Additional weather informations (UV, air quality, humidity, etc.)
- Mutliple locations weather information

## Requirements
- .NET 6.0 or later
- API Key from a weather API service 

## Installation & Setup

1. **Clone the Repository**
   ```sh
   git clone https://github.com/Rale7/VremenskaPrognoza.git
   cd weather-app
   ```

2. **Install Dependencies** (if needed)
   ```sh
   dotnet restore
   ```

3. **Set Up API Key**
   - You should should first create account on [weather API service](https://www.weatherapi.com/), you can create free account here. Then on your profile you will see your API key
   - Create a `.env` file in the root directory (or update `appsettings.json`)
   - Add the following line:
     ```sh
     API_KEY=your_api_key_here
     ```
   - Alternatively, you can set an environment variable:
     ```sh
     export API_KEY=your_api_key_here
     ```

4. **Run the Application**
   ```sh
   dotnet run
   ```
5. Alternatively if you have Visual Studio you can clone it from there and you will get all dependencies, but you will still have to add .env file with API_KEY.

## Usage
- Enter a city name to fetch weather details.
- The UI will display animations and real-time weather information.

## Deployment
To publish the app as an executable:
```sh
dotnet publish -c Release -r win-x64 --self-contained true
```

## Credits
![Alt text](//cdn.weatherapi.com/v4/images/weatherapi_logo.png) <br>
Weather data provided by [weather API](https://www.weatherapi.com/)

