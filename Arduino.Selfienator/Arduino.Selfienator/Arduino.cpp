const int CLOCK_WISE = 0;
const int COUNTER_CLOCK_WISE = 1;

const int commCamera = 8;
const int shotPin = 7;
const int focusPin = 6;

int motorX[] = {
	0b00111001,
	0b00101000,
	0b00111010,
	0b00010010,
	0b00110110,
	0b00100100,
	0b00110101,
	0b00010001,
};

int motorXres = 0b11000000;

int motorY[] = {
	0b00001000,
	//0b00101100,
	0b00000100,
	//0b00110110,
	0b00000010,
	//0b00010011,
	0b00000001
	//,0b00111001 
};

int motorYres = 0b11110000;

int motorYCon[] = {
	0b10000000,
	//0b00101100,
	0b10000000,
	//0b00110110,
	0b01000000,
	//0b00010011,
	0b01000000
	//,0b00111001 
};

int motorYConres = 0b00111111;

bool x_isExecuting = false;
double x_steepAngle = 0.9;
double x_angle = 0;
double x_goalAngle = 0;
int x_goalDelay = 0;
int x_goalDirection = CLOCK_WISE;
int x_deltaTime = 0;
int x_LastTime = 0;
int x_stage = 0;

bool y_isExecuting = false;
double y_steepAngle = 1;
double y_angle = 180;
double y_goalAngle = 90;
int y_goalDelay = 0;
int y_goalDirection = CLOCK_WISE;
int y_deltaTime = 0;
int y_LastTime = 0;
int y_stage = 0;

bool focus = false;
bool focusDown = false;
int focusTime = 2000;
int focusTimeDown = 0;

bool shot = false;
bool shotDown = false;
int shotTime = 600;
int shotTimeDown = 0;

String inData;
String array[2][4];

void setup()
{
	Serial.begin(9600);
	pinMode(commCamera, OUTPUT);
	pinMode(shotPin, OUTPUT);
	pinMode(focusPin, OUTPUT);
	digitalWrite(commCamera, 1);
	digitalWrite(focusPin, 1);
	digitalWrite(shotPin, 1);
	pinMode(13, OUTPUT);
	DDRF |= 0b11111111;
	DDRK |= 0b00001111;
}

void loop()
{
	proces();
}

void serialEvent(){
	while (Serial.available() > 0)
	{

		char recieved = Serial.read();

		if (recieved == ';')
		{
			parseData();
			//Serial.print("Arduino Received: ");
			//Serial.print(inData);
			inData = "";
		}
		else if (recieved != '\n'){
			inData += recieved;
		}
	}
}

void setData(int i, int j){

	//Serial.println("array:");
	//Serial.println("1" + array[0][0] + " ");
	//Serial.println("2" + array[0][1] + " ");
	//Serial.println("3" + array[0][2] + " ");
	//Serial.println("4" + array[0][3] + " ");
	//Serial.println("5" + array[1][0] + " ");
	//Serial.println("6" + array[1][1] + " ");
	//Serial.println("7" + array[1][2] + " ");
	//Serial.println("8" + array[1][3]);

	for (int i = 0; i < 2; ++i)
	{
		if (array[i][0] == "MX")
		{
			x_goalAngle = array[i][1].toFloat();
			x_goalDirection = array[i][2].toInt();
			x_goalDelay = array[i][3].toInt();
			x_isExecuting = true;
			x_deltaTime = x_goalDelay;
			Serial.println("MX;");
		}
		if (array[i][0] == "MY")
		{
			y_goalAngle = array[i][1].toFloat();
			y_goalDirection = array[i][2].toInt();
			y_goalDelay = array[i][3].toInt();
			y_isExecuting = true;
			y_deltaTime = y_goalDelay;
			Serial.println("MY;");
		}
		if (array[i][0] == "F")
		{
			focus = true;
			focusTimeDown = millis() + focusTime;
			Serial.println("F;");
		}
		if (array[i][0] == "S")
		{
			shot = true;
			shotTimeDown = millis() + shotTime;
			Serial.println("S;");
		}
	}
}

void parseData(){
	clearArray();
	int i = 0, j = 0;
	inData.trim();
	int index = inData.indexOf(",");
	while ((index >= 0) || (inData.length() > 0)){
		String tmp;
		if (index == -1){
			tmp = "";
		}
		else {
			tmp = inData.substring(index + 1);
			inData = inData.substring(0, index);
		}
		String inData2 = inData;
		int index2 = inData.indexOf("-");
		while ((index2 >= 0) || (inData2.length() > 0)){
			String tmp2;
			if (index2 == -1){
				tmp2 = "";
			}
			else {
				tmp2 = inData2.substring(index2 + 1);
				inData2 = inData2.substring(0, index2);
			}

			//setData(inData2, i, j);
			array[i][j] = inData2;

			j++;
			inData2 = tmp2;
			index2 = inData2.indexOf("-");
		}
		j = 0;
		i++;
		inData = tmp;
		index = inData.indexOf(",");
	}
	setData(i, j);
}

// MX-180-0-5,MY-45-1-6;

void clearArray()
{
	for (int i = 0; i < 2; ++i)
	{
		for (int j = 0; j < 4; ++j)
		{
			array[i][j] = "";
		}
	}
}

void proces()
{
	if (x_isExecuting)
	{
		if (abs(x_goalAngle - x_angle) < x_steepAngle)
		{
			x_goalAngle = x_angle;
		}
		else if (x_goalAngle != x_angle)
		{

			if (x_deltaTime >= x_goalDelay)
			{
				if (x_goalDirection == CLOCK_WISE)
				{
					x_angle += x_steepAngle;

					Serial.println("xAngle:" + String(x_angle));

					PORTF &= motorXres;

					Serial.println(PORTF, BIN);

					x_stage = (++x_stage) % 8;

					PORTF |= motorX[x_stage];

					Serial.println(PORTF, BIN);
					Serial.println(x_stage);

					//MOVE
				}
				else if (x_goalDirection == COUNTER_CLOCK_WISE)
				{
					x_angle -= x_steepAngle;

					Serial.println("xAngle:" + String(x_angle));

					PORTF &= motorXres;

					Serial.println(PORTF, BIN);

					x_stage = (--x_stage);

					if (x_stage < 0){
						x_stage = 7;
					}

					PORTF |= motorX[x_stage];

					Serial.println(PORTF, BIN);
					Serial.println(x_stage);

					//MOVE
				}
				x_deltaTime = 0;
				x_LastTime = millis();
				double a = x_deltaTime;
			}
			x_deltaTime = (millis() - x_LastTime);
		}
		if (x_goalAngle == x_angle)
		{
			PORTF &= motorXres;
			x_isExecuting = false;
		}
	}
	if (y_isExecuting)
	{
		if (abs(y_goalAngle - y_angle) < y_steepAngle)
		{
			y_angle = y_goalAngle;
		}
		else if (y_goalAngle != y_angle)
		{
			if (y_deltaTime >= y_goalDelay)
			{
				if (y_goalDirection == CLOCK_WISE)
				{
					y_angle += y_steepAngle;

					Serial.println("yAngle:" + String(y_angle));

					PORTF &= motorYConres;
					PORTK &= motorYres;

					Serial.println(PORTF, BIN);
					Serial.println(PORTK, BIN);

					y_stage = (++y_stage) % 4;

					PORTF |= motorYCon[y_stage];
					PORTK |= motorY[y_stage];

					Serial.println(PORTF, BIN);
					Serial.println(PORTK, BIN);
					Serial.println(y_stage);

					//MOVE

				}
				else if (y_goalDirection == COUNTER_CLOCK_WISE)
				{
					y_angle -= y_steepAngle;

					Serial.println("yAngle:" + String(y_angle));

					PORTF &= motorYConres;
					PORTK &= motorYres;

					Serial.println(PORTF, BIN);
					Serial.println(PORTK, BIN);

					if (y_stage < 0){
						y_stage = 3;
					}

					PORTF |= motorYCon[y_stage];
					PORTK |= motorY[y_stage];

					Serial.println(PORTF, BIN);
					Serial.println(PORTK, BIN);
					Serial.println(y_stage);
					//MOVE
				}
				y_deltaTime = 0;
				y_LastTime = millis();
				double a = y_deltaTime;
			}
			y_deltaTime = (millis() - y_LastTime);
		}
		if (y_goalAngle == y_angle)
		{
			y_isExecuting = false;
		}
	}

	if (focus == true)
	{
		digitalWrite(focusPin, 0);
		Serial.println("FF;");
		focus = false;
		focusDown = true;
	}
	else if (focusDown == true)
	{
		if (focusTimeDown < millis())
		{
			digitalWrite(focusPin, 1);
			focusDown = false;
		}
	}
	else if (shot == true)
	{
		digitalWrite(shotPin, 0);
		delay(100);
		digitalWrite(focusPin, 0);
		Serial.println("SS;");
		shot = false;
		shotDown = true;
	}
	else if (shotDown == true)
	{
		if (shotTimeDown < millis())
		{
			Serial.println("SEnd;");
			digitalWrite(shotPin, 1);
			digitalWrite(focusPin, 1);
			shotDown = false;
		}
	}
}