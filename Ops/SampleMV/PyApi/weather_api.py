from flask import Flask, request, jsonify
from datetime import datetime, timedelta
import random

app = Flask(__name__)

@app.route('/weather', methods=['GET'])
def get_mock_weather():
    town_name = request.args.get('town')
    
    if not town_name:
        return jsonify({'error': 'Town name is missing'}), 400
    
    forecast_data = generate_mock_weather()
    
    return jsonify({'town': town_name, 'forecast': forecast_data}), 200

def generate_mock_weather():
    # Get today's date
    today = datetime.today().date()
    # Create a dictionary to hold forecast data organized by day
    mock_forecast_by_day = {}

    # Generate forecast data for the next four days
    for day in range(0, 4):
        forecast_date = today + timedelta(days=day)
        forecast_points = []

        # Generate forecast points for specific hours
        for hour in [8, 12, 16, 20]:
            # Generate a random temperature between -5°C and 30°C
            random_temperature = random.uniform(-5, 30)
            random_temperature = int(random_temperature)

            # Determine weather description based on temperature
            if random_temperature < 5:
                random_weather = random.choice(['Sunny', 'Cloudy', 'Rainy', 'Partly Cloudy', 'Snowy', 'Hail storms', 'Thunderstorms', 'Fog'])
            else:
                random_weather = random.choice(['Sunny', 'Cloudy', 'Rainy', 'Partly Cloudy', 'Thunderstorms'])

            # Create a forecast point with datetime, temperature, and weather description
            forecast_points.append({
                'hour': hour,
                'temperatureInCelsius': random_temperature,
                'weather': random_weather
            })

        # Store forecast points for the day in the dictionary
        mock_forecast_by_day[forecast_date.strftime('%Y-%m-%d')] = forecast_points
    
    return mock_forecast_by_day

if __name__ == '__main__':
    # Run the app on host '0.0.0.0' and port 7210 inside the container
    app.run(host='0.0.0.0', port=7210, debug=True)
