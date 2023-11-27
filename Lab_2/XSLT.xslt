<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>

  <xsl:template match="ScientificWorkers">
    <html>
      <body>
        <table border="1">
          <tr>
            <th>Name</th>
			<th>Author Name</th>
			<th>Author Position</th>
            <th>Faculty</th>
            <th>Department</th>
            <th>Date Of Birth</th>
			<th>Gender</th>
			<th>Address</th>
			<th>Age</th>
			<th>Branch</th>
          </tr>
          <xsl:for-each select="ScientificWorker">
            <tr>
				<td>
					<xsl:value-of select ="Name"/>
				</td>
				<td>
					<xsl:value-of select ="AuthorName"/>
				</td>
				<td>
					<xsl:value-of select ="AuthorPosition"/>
				</td>
				<td>
					<xsl:value-of select ="Faculty"/>
				</td>
				<td>
					<xsl:value-of select ="Department"/>
				</td>
				<td>
					<xsl:value-of select ="DateOfBirth"/>
				</td>
				<td>
					<xsl:value-of select ="Gender"/>
				</td>
				<td>
					<xsl:value-of select ="Address"/>
				</td>
				<td>
					<xsl:value-of select ="Age"/>
				</td>
				<td>
					<xsl:value-of select ="Branch"/>
				</td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
