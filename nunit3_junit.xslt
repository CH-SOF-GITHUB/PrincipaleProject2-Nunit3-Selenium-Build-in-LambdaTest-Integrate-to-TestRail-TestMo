<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>

	<!-- Racine -->
	<xsl:template match="/test-run">
		<testsuites tests="{@testcasecount}" failures="{@failed}" disabled="{@skipped}" time="{@duration}">
			<xsl:apply-templates select="test-suite"/>
		</testsuites>
	</xsl:template>

	<!-- Traiter chaque test-suite -->
	<xsl:template match="test-suite">
		<xsl:if test="test-case or test-suite">
			<testsuite tests="{@testcasecount}" time="{@duration}" errors="{@testcasecount - @passed - @skipped - @failed}" failures="{@failed}" skipped="{@skipped}" timestamp="{@start-time}">
				<!-- Construction du nom du testsuite -->
				<xsl:attribute name="name">
					<xsl:choose>
						<xsl:when test="ancestor-or-self::test-suite[@type='TestSuite'][@name] and string-length(@name) > 0">
							<xsl:for-each select="ancestor-or-self::test-suite[@type='TestSuite'][@name]/@name">
								<xsl:value-of select="concat(., '.')"/>
							</xsl:for-each>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>UnnamedTestSuite</xsl:text>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:attribute>
				<!-- traiter les cas -->
				<xsl:apply-templates select="test-case"/>
				<!-- traiter la récursion pour sous-suites -->
				<xsl:apply-templates select="test-suite"/>
			</testsuite>
		</xsl:if>
	</xsl:template>

	<!-- Traitement des test-case -->
	<xsl:template match="test-case">
		<testcase name="{@name}" assertions="{@asserts}" time="{@duration}" status="{@result}" classname="{@classname}">
			<xsl:if test="@runstate = 'Skipped' or @runstate = 'Ignored'">
				<skipped/>
			</xsl:if>
			<!-- autres éléments si nécessaire -->
		</testcase>
	</xsl:template>

</xsl:stylesheet>