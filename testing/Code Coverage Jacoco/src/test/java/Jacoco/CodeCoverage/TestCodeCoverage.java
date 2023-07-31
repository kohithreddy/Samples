package Jacoco.CodeCoverage;

import org.testng.annotations.Test;

import compare.CompareString;

public class TestCodeCoverage {
	
	@Test
	public void PostiveTests()
	{
		CompareString obj1=new CompareString();
		obj1.containsornot("Automation");
	}
	@Test
	public void NegativeTests()
	{
		CompareString obj1=new CompareString();
		obj1.containsornot("NegativeTesting");
	}

}
