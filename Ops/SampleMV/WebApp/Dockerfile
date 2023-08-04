# Use an official Node.js runtime as the base image
FROM node:19

# Set the working directory
WORKDIR /usr/src/app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install app dependencies
RUN npm install

# Copy app source code to the container
COPY . .

# Expose port 4200 for the Angular dev server
EXPOSE 6400

# Start the Angular dev server
CMD ["npm", "start"]
