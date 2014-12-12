/*
  Harry Potter Interactive Wands
  
  Made with Little Bits
  Copied and only slightly modified from the Scent-imental Notification System
  http://www.instructables.com/id/Scent-imental-Notification-System/
 
  This example code is in the public domain.
 */
 
 int cloudValue = 0;
 
//Give unique names to the 3 digital output pins of the Arduino
int incendioPin  = 1;
int locomotorPin = 5;
int aguamentiPin = 9;

// The setup routine sets up the Arduino and runs once
void setup() {    
  
  // Initialize the digital pins as output pins.
  pinMode(incendioPin,  OUTPUT);
  pinMode(locomotorPin, OUTPUT);  
  pinMode(aguamentiPin, OUTPUT);    
  
  //uncomment the line below for debugging
  //Serial.begin(9600); 

}

// The loop routine runs over and over forever
void loop() {
  
  //Read the analog input from the Cloudbit
  cloudValue = analogRead(A0);
  
  //Convert the analog value of 0 to 1023 to a number between 0 and 100
  cloudValue = map(cloudValue, 0, 1023, 0, 100); 
  
  //uncomment the line below for debugging
  //Serial.println(cloudValue);   
  
  //Check for values from the Cloudbit
  
  //If the number is 25, trigger incendio
  if((cloudValue > 10) &&  (cloudValue < 33)){    
    digitalWrite(incendioPin,  HIGH);  
    digitalWrite(locomotorPin, LOW);
    digitalWrite(aguamentiPin, LOW);
  } 

  //If the number is 50, trigger locomotor  
  else if((cloudValue >= 33) && (cloudValue < 66)){ 
    digitalWrite(incendioPin,  LOW);
    digitalWrite(locomotorPin, HIGH);  
    digitalWrite(aguamentiPin, LOW); 
  } 

  //If the number is 75, trigger agumenti  
  else if((cloudValue >= 66) && (cloudValue < 100)){ 
    digitalWrite(incendioPin,  LOW);
    digitalWrite(locomotorPin, LOW);
    digitalWrite(aguamentiPin, HIGH);  
  } 
    
  //If the number is 0, trigger nothing
  else if(cloudValue <= 10){
    digitalWrite(incendioPin,  LOW);
    digitalWrite(locomotorPin, LOW);
    digitalWrite(aguamentiPin, LOW);    
  }

}
