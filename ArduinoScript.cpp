#include <Ultrasonic.h>
 
//trig e poi echo
Ultrasonic ultrasonic(10, 8);
int distance;
 
 
void setup() {
  Serial.begin(9600);
 
}
 
void loop() {
  // Pass INC as a parameter to get the distance in inches
  distance = ultrasonic.read();
  
  delay(3);
  Serial.println(distance);
  
  
}
