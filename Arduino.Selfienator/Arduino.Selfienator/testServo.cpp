#include <Servo.h> 

Servo myservo;  // create servo object to control a servo 

int myservoPin = 4;

void setup()
{
	Serial.begin(9600);
	myservo.attach(myservoPin);
}


void loop()
{
	for (int i = 0; i < 180; i++)
	{
		myservo.write(i);
		sleep(25);
	}
	for (int i = 180; i > 0; i--)
	{
		myservo.write(i);
		sleep(25);
	}
}