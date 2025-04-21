FROM node:16

WORKDIR /app

# Copy package.json only
COPY package.json ./

# Run npm install without requiring package-lock.json
RUN npm install

# Copy the rest of the application
COPY . .

# Build the application
RUN npm run build

# Install serve for production hosting
RUN npm install -g serve

# Expose port 3000
EXPOSE 3000

# Command to run the application
CMD ["serve", "-s", "build"]